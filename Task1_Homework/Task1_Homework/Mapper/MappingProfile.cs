using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;
using Task1_Homework.Controllers.Api.Models;

namespace Task1_Homework.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventResource>()
                .ForMember(ev => ev.Id, opt => opt.MapFrom(er => er.Id))
                .ForMember(ev => ev.Name, opt => opt.MapFrom(er => er.Name))
                .ForMember(ev => ev.VenueId, opt => opt.MapFrom(er => er.VenueId))
                .ForMember(ev => ev.Banner, opt => opt.MapFrom(er => er.Banner))
                .ForMember(ev => ev.Description, opt => opt.MapFrom(er => er.Description))
                .ForMember(ev => ev.Date, opt => opt.MapFrom(er => er.Date))
                .ForMember(ev => ev.Tickets, opt => opt.MapFrom(er => er.Tickets))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Venue, VenueResource>()
                .ForMember(v => v.Id, opt => opt.MapFrom(vr => vr.Id))
                .ForMember(v => v.Name, opt => opt.MapFrom(vr => vr.Name))
                .ForMember(v => v.CityId, opt => opt.MapFrom(vr => vr.CityId))
                .ForMember(v => v.Adress, opt => opt.MapFrom(vr => vr.Adress))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
