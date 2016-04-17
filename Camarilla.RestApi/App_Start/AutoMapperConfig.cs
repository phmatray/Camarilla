using AutoMapper;
using Camarilla.RestApi.Controllers.ControllerModels;
using Camarilla.RestApi.Infrastructure.Helpers;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserReturnModelLite>();
                cfg.CreateMap<User, UserReturnModel>()
                    .ForMember(
                        dest => dest.FullName,
                        opts => opts.MapFrom(src => $"{src.FirstName} {src.LastName}"));

                cfg.CreateMap<IdentityRole, RoleReturnModelLite>();
                cfg.CreateMap<IdentityRole, RoleReturnModel>();

                cfg.CreateMap<Race, RaceReturnModelLite>();
                cfg.CreateMap<Race, RaceReturnModel>();

                cfg.CreateMap<Clan, ClanReturnModelLite>();
                cfg.CreateMap<Clan, ClanReturnModel>()
                    .ForMember(
                        dest => dest.Category,
                        opts => opts.MapFrom(src => src.ClanCategory.GetDisplayName()))
                    .ForMember(
                        dest => dest.Kind,
                        opts => opts.MapFrom(src => src.ClanKind.GetDisplayName()));

                cfg.CreateMap<Persona, PersonaReturnModelLite>();
                cfg.CreateMap<Persona, PersonaReturnModel>()
                    .ForMember(
                        dest => dest.Gender,
                        opts => opts.MapFrom(src => src.PersonaGender.GetDisplayName()));
                cfg.CreateMap<Persona, PersonaWithMailReturnModel>();
                cfg.CreateMap<Persona, PersonaWithAllReturnModel>();

                cfg.CreateMap<Mail, MailReturnModelLite>();
                cfg.CreateMap<Mail, MailReturnModel>();

                cfg.CreateMap<PersonaMail, PersonaMailReturnModel>();
            });
        }
    }
}