using AutoMapper;
using UserService.Core.DbEntities;
using UserService.Infrastructure.Mapper.Converters;

namespace UserService.Infrastructure.Mapper;

public static class MapperExtensions
{
    public static Account ToDto(this UserDbEntity userDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDbEntity, Account>()
                .ConvertUsing<UserDbEntityToAccountConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<UserDbEntity, Account>(userDbEntity);
    }

    public static ICollection<Account> ToDto(this ICollection<UserDbEntity> userDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDbEntity, Account>()
                .ConvertUsing<UserDbEntityToAccountConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<ICollection<UserDbEntity>, ICollection<Account>>(userDbEntity);
    }
}