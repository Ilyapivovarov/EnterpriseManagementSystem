using AutoMapper;

namespace IdentityService.Application.Mapper;

public static class MapperExtensions
{
    public static UserDto ToDto(this User user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<User, UserDto>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<User, UserDto>(user);
    }

    public static SessionDto ToDto(this Session user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<Session, SessionDto>()
                .ForMember(dto => dto.UserGuid,
                    source 
                        => source.MapFrom(c => c.User.Guid)));

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<Session, SessionDto>(user);
    }
}