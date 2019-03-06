using AutoMapper;
using BussinesFacade;
using Web.Models;

namespace Web
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<Data, CurrencyViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));
        }
    }
}