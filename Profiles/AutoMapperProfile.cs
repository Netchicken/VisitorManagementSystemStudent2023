using AutoMapper;

using VisitorManagementSystem.Models;
using VisitorManagementSystem.ViewModels;

namespace VisitorManagementSystem.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Visitors, VisitorsVM>().ReverseMap();
            CreateMap<StaffNames, StaffNamesVM>().ReverseMap();

        }

    }
}
