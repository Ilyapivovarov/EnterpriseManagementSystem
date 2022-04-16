using AutoMapper;
using Session = EnterpriseManagementSystem.Contracts.WebContracts.Session;

namespace IdentityService.Application.Mapper;

public static class MapperExtensions
{
    public static Account ToDto(this User user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<User, Account>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<User, Account>(user);
    }

    public static Session ToDto(this Models.Session user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<Models.Session, Session>()
                .ForMember(dto => dto.UserGuid,
                    source 
                        => source.MapFrom(c => c.User.Guid)));

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<Models.Session, Session>(user);
    }
}