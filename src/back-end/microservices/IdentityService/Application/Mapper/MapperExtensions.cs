using AutoMapper;
using Session = EnterpriseManagementSystem.Contracts.WebContracts.Session;

namespace IdentityService.Application.Mapper;

public static class MapperExtensions
{
    public static Account ToDto(this UserDbEntity user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDbEntity, Account>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<UserDbEntity, Account>(user);
    }

    public static Session ToDto(this Models.SessionDbEntity user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<Models.SessionDbEntity, Session>()
                .ForMember(dto => dto.UserGuid,
                    source 
                        => source.MapFrom(c => c.User.Guid)));

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<Models.SessionDbEntity, Session>(user);
    }
}