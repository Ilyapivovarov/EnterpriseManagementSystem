using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
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
    
    public static SessionDto ToDto(this IJwtSession session)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<IJwtSession, SessionDto>()
                .ForMember(dto => dto.RefreshToken,
                    source => source.MapFrom(x => x.RefreshToken.ToString()))
                .ForMember(dto => dto.AccessToken, 
                    source => source.MapFrom(x => x.AccessToken.ToString())));

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<IJwtSession, SessionDto>(session);
    }
}