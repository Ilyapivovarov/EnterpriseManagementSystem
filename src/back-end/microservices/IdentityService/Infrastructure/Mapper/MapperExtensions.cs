using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
using IdentityService.Infrastructure.Mapper.Converters;

namespace IdentityService.Infrastructure.Mapper;

public static class MapperExtensions
{
    public static SessionDto ToDto(this SessionDbEntity user)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<SessionDbEntity, SessionDto>()
                .ForMember(dto => dto.UserGuid,
                    source
                        => source.MapFrom(c => c.User.Guid)));

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<SessionDbEntity, SessionDto>(user);
    }
}