using AutoMapper;
using Cade.BookStore.Books;
using Cade.BookStore.Dtos;

namespace Cade.BookStore
{
    public class BookStoreApplicationAutoMapperProfile : Profile
    {
        public BookStoreApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            //application -> presentation
            CreateMap<Book, ReadBookDto>();

            //application -> presentation
            CreateMap<CreateUpdateBookDto, Book>();
        }
    }
}
