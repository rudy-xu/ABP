using Cade.BookStore.Dtos;
using Cade.BookStore.Permissions;    //BookStorePermissions
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cade.BookStore.Books
{
    /*
     *implements all the CRUD
        CrudAppService<
            Book,              //The Book entity
            ReadBookDto,       //Used to show books
            Guid,              //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>        //Used to create/update a book
     */
    //uses IObjectMapper service (see) to map Book objects to BookDto objects and CreateUpdateBookDto objects to Book objects. 
    public class BookAppService : CrudAppService<Book, ReadBookDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookDto>, IBookAppService 
    {
        //injects IRepository<Book, Guid> which is the default repository for the Book entity
        //ABP automatically creates default repositories for each aggregate root (or entity)
        public BookAppService(IRepository<Book, Guid> repository): base(repository)
        {
            GetPolicyName = BookStorePermissions.Books.Default;
            GetListPolicyName = BookStorePermissions.Books.Default;
            CreatePolicyName = BookStorePermissions.Books.Create;
            UpdatePolicyName = BookStorePermissions.Books.Update;
            DeletePolicyName = BookStorePermissions.Books.Delete;
        }
    }
}
