using AutoMapper;
using Cade.BookStore.Dtos;

namespace Cade.BookStore.Web
{
    public class BookStoreWebAutoMapperProfile : Profile
    {
        public BookStoreWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<ReadBookDto, CreateUpdateBookDto>();
        }
    }
}
