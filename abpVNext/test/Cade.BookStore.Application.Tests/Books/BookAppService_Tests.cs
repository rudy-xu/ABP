using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;   //PagedAndSortedResultRequestDto
using Cade.BookStore.Dtos;     //CreateUpdateBookDto
using System.Threading.Tasks;
using Volo.Abp.Validation;     //Exception
using System.Linq;
using Shouldly;      //ShouldBeGreaterThan
using Xunit;

namespace Cade.BookStore.Books
{
    public class BookAppService_Tests : BookStoreApplicationTestBase
    {
        //Preparation
        private readonly IBookAppService _bookAppService;

        public BookAppService_Tests()
        {
            _bookAppService = GetRequiredService<IBookAppService>();
        }

        //test _bookAppService GetList
        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            //Action
            var result = await _bookAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "1984");
        }

        //test _bookAppService create
        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            //Action
            var result = await _bookAppService.CreateAsync(
                    new CreateUpdateBookDto
                    {
                        Name = "The Kite Runner",
                        Price = 90,
                        PublishDate = DateTime.Now,
                        Type = BookType.Fantastic
                    }
                );

            //Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("The Kite Runner");
        }

        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            //Action
            var exception = await Assert.ThrowsAsync<AbpValidationException>(
                    async () => 
                    {
                        await _bookAppService.CreateAsync(new CreateUpdateBookDto 
                            {
                                Name = "",
                                Price = 10,
                                PublishDate = DateTime.Now,
                                Type = BookType.ScienceFiction
                            }
                        );
                    }
                );

            exception.ValidationErrors.ShouldContain(err => err.MemberNames.Any(m => m == "Name"));
        }
    }
}
