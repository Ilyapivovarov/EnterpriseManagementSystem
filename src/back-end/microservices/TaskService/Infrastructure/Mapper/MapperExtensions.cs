using EnterpriseManagementSystem.Contracts.Common;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
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

    public static RecordsCollection<TaskDto> ToDto(this ICollection<TaskDbEntity>? taskDbEntity)
    {
        if (taskDbEntity == null)
            return RecordsCollection<TaskDto>.Empty;

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
            return RecordsCollection<UserDto>.Empty;

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
            return RecordsCollection<TaskStatusDto>.Empty;

        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<TaskStatusDbEntity, TaskStatusDto>()
                .ConvertUsing<TaskStatusToDtoConverter>());

        var mapper = new AutoMapper.Mapper(cfg);
        return mapper.Map<ICollection<TaskStatusDbEntity>, RecordsCollection<TaskStatusDto>>(taskStatusDbEntities);
    }
}
