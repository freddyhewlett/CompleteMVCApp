using AutoMapper;
using FH.App.ViewModels;
using FH.Business.Models;

namespace FH.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Developer, DeveloperViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Game, GameViewModel>().ReverseMap();
        }
    }
}
