using EnterpriseManagementSystem.Contracts.Common;
using TaskService.Infrastructure.Mapper.Converters;

namespace TaskService.Infrastructure.Mapper;

public static class MapperExtensions
{
    public static TaskDto ToDto(this TaskDbEntity taskDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<TaskDbEntity, TaskDto>()
                .ConvertUsing<TaskDbEntityToDtoConverter>());

        var mapper = new AutoMapper.Mapper(cfg);

        return mapper.Map<TaskDbEntity, TaskDto>(taskDbEntity);
    }

    public static RecordsCollection<TaskDto> ToDto(this ICollection<TaskDbEntity> taskDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<TaskDbEntity, TaskDto>()
                .ConvertUsing<TaskDbEntityToDtoConverter>());

        var mapper = new AutoMapper.Mapper(cfg);

        return mapper.Map<ICollection<TaskDbEntity>, RecordsCollection<TaskDto>>(taskDbEntity);
    }

    public static UserDto ToDto(this UserDbEntity userDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDbEntity, UserDto>()
                .ConvertUsing<UserDbEntityToDtoConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<UserDbEntity, UserDto>(userDbEntity);
    }

    public static RecordsCollection<UserDto> ToDto(this ICollection<UserDbEntity>? userDbEntities)
    {
        if (userDbEntities == null)
            return new RecordsCollection<UserDto>();

        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDbEntity, UserDto>()
                .ConvertUsing<UserDbEntityToDtoConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<ICollection<UserDbEntity>, RecordsCollection<UserDto>>(userDbEntities);
    }

    public static TaskStatusDto ToDto(this TaskStatusDbEntity taskStatusDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<TaskStatusDbEntity, TaskStatusDto>()
                .ConvertUsing<TaskStatusToDtoConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<TaskStatusDbEntity, TaskStatusDto>(taskStatusDbEntity);
    }

    public static RecordsCollection<TaskStatusDto> ToDto(this ICollection<TaskStatusDbEntity>? taskStatusDbEntities)
    {
        if (taskStatusDbEntities == null)
            return new RecordsCollection<TaskStatusDto>();

        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<TaskStatusDbEntity, TaskStatusDto>()
                .ConvertUsing<TaskStatusToDtoConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<ICollection<TaskStatusDbEntity>, RecordsCollection<TaskStatusDto>>(taskStatusDbEntities);
    }
}
