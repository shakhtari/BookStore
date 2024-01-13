using AutoMapper;
using BookStore.Books;
using BookStore.Publications;

namespace BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        CreateMap<Book,BookDto>();
        CreateMap<Publication,PublicationDto>();
    }
}
