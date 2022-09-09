using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
using IdentityService.Infrastructure.Mapper.Converters;

namespace IdentityService.Infrastructure.Mapper;

public static class MapperExtensions
{
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