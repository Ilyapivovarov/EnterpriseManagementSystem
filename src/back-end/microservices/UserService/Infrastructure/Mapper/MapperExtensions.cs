using UserService.Infrastructure.Mapper.Converters;

namespace UserService.Infrastructure.Mapper;

public static class MapperExtensions
{
    public static UserDataResponse ToDto(this UserDbEntity userDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDbEntity, UserDataResponse>()
                .ConvertUsing<UserDbEntityToAccountConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<UserDbEntity, UserDataResponse>(userDbEntity);
    }

    public static ICollection<UserDataResponse> ToDto(this ICollection<UserDbEntity> userDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDbEntity, UserDataResponse>()
                .ConvertUsing<UserDbEntityToAccountConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<ICollection<UserDbEntity>, ICollection<UserDataResponse>>(userDbEntity);
    }

    public static PositionDataResponse ToDto(this PositionDbEntity positionDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<PositionDbEntity, PositionDataResponse>()
                .ConvertUsing<PositionDbEntityToPositionDataResponse>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<PositionDbEntity, PositionDataResponse>(positionDbEntity);
    }

    public static ICollection<PositionDataResponse> ToDto(this ICollection<PositionDbEntity> positionDbEntity)
    {
        throw new NotImplementedException();
    }

    public static EmployeeDataResponse ToDto(this EmployeeDbEntity employeeDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<EmployeeDbEntity, EmployeeDataResponse>()
                .ConvertUsing<EmployeeDbEntityToEmployeeDataResponseConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<EmployeeDbEntity, EmployeeDataResponse>(employeeDbEntity);
    }

    public static ICollection<EmployeeDataResponse>? ToDto(this ICollection<EmployeeDbEntity> employeeDbEntities)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<EmployeeDbEntity, EmployeeDataResponse>()
                .ConvertUsing<EmployeeDbEntityToEmployeeDataResponseConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<ICollection<EmployeeDbEntity>, ICollection<EmployeeDataResponse>>(employeeDbEntities);
    }
}