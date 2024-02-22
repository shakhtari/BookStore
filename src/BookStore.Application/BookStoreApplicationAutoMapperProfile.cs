using AutoMapper;
using BookStore.Books;
using BookStore.DerivedValues;
using BookStore.DerivedValueDetails;
using BookStore.Formulas;
using BookStore.MimicDiagrams;
using BookStore.MimicProfiles;
using BookStore.Publications;

namespace BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        CreateMap<Book,BookDto>();
        CreateMap<Publication,PublicationDto>();
        CreateMap<MimicProfile, MimicProfileDto>();
        CreateMap<MimicDiagram, MimicProfileDto>();
        CreateMap<Formula, FormulaDto>();
        CreateMap<DerivedValue, DerivedValueDto>();
        CreateMap<DerivedValueDetail, DerivedValueDetailDto>();
    }
}
