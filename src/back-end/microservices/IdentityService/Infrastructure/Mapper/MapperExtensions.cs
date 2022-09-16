using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using IdentityService.Infrastructure.Mapper.Converters;

namespace IdentityService.Infrastructure.Mapper;

public static class MapperExtensions
{
    public static IdentityUserDto ToDto(this UserDbEntity user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDbEntity, IdentityUserDto>()
                .ConvertUsing<UserDbEntityToIdentityUserDto>());

        var mapper = new AutoMapper.Mapper(cfg);

        return mapper.Map<UserDbEntity, IdentityUserDto>(user);
    }
    
    public static SessionDto ToDto(this Session session)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<Session, SessionDto>()
                .ForMember(dto => dto.UserGuid,
                    source
                        => source.MapFrom(c => c.User.Guid)));

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<Session, SessionDto>(session);
    }
}