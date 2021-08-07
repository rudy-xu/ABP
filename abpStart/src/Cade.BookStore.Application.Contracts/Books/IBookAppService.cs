using Cade.BookStore.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Cade.BookStore.Books
{
    /*
     * Defines CRUD methods
     *     ICrudAppService<
     *          ReadBookDto,      //Used to show books
     *          Guid,             //Primary key of the book entity
     *          PagedAndSortedResultRequestDto, //Used for paging/sorting
     *          CreateUpdateBookDto>     //Used to create/update a book                                         
     */
    public interface IBookAppService : ICrudAppService<ReadBookDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookDto>
    { 
    
    }
}
