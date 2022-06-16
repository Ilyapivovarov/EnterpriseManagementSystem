using AutoMapper;
using IdentityService.Infrastructure.Mapper.Converters;

namespace IdentityService.Infrastructure.Mapper;

public static class MapperExtensions
{
    public static Account ToDto(this UserDbEntity user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDbEntity, Account>()
                .ConvertUsing<UserDbEntityToAccountConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<UserDbEntity, Account>(user);
    }

    public static Session ToDto(this SessionDbEntity user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<SessionDbEntity, Session>()
                .ForMember(dto => dto.UserGuid,
                    source
                        => source.MapFrom(c => c.User.Guid)));

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<SessionDbEntity, Session>(user);
    }
}