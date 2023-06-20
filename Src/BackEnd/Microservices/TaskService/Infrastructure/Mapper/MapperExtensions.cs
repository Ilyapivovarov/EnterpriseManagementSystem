using EnterpriseManagementSystem.Contracts.Common;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using TaskService.Infrastructure.Mapper.Converters;

namespace TaskService.Infrastructure.Mapper;

public static class MapperExtensions
{
    private static readonly IMapper Mapper;

    static MapperExtensions()
    {
        var cfg = new MapperConfigurationExpression();

        cfg.CreateMap<TaskDbEntity, TaskDto>()
            .ConvertUsing<TaskDbEntityToDtoConverter>();

        cfg.CreateMap<UserDbEntity, UserDto>()
            .ConvertUsing<UserDbEntityToDtoConverter>();

        cfg.CreateMap<TaskStatusDbEntity, TaskStatusDto>()
            .ConvertUsing<TaskStatusToDtoConverter>();

        var mapperConfiguration = new MapperConfiguration(cfg);

        Mapper = mapperConfiguration.CreateMapper();
    }

    public static TaskDto ToDto(this TaskDbEntity taskDbEntity) 
        => Mapper.Map<TaskDbEntity, TaskDto>(taskDbEntity);

    public static RecordsCollection<TaskDto> ToDto(this ICollection<TaskDbEntity>? taskDbEntity) 
        => taskDbEntity == null
            ? RecordsCollection<TaskDto>.Empty
            : Mapper.Map<ICollection<TaskDbEntity>, RecordsCollection<TaskDto>>(taskDbEntity);

    public static UserDto ToDto(this UserDbEntity userDbEntity) 
        => Mapper.Map<UserDbEntity, UserDto>(userDbEntity);

    public static RecordsCollection<UserDto> ToDto(this ICollection<UserDbEntity>? userDbEntities) 
        => userDbEntities == null
            ? RecordsCollection<UserDto>.Empty
            : Mapper.Map<ICollection<UserDbEntity>, RecordsCollection<UserDto>>(userDbEntities);

    public static TaskStatusDto ToDto(this TaskStatusDbEntity taskStatusDbEntity) 
        => Mapper.Map<TaskStatusDbEntity, TaskStatusDto>(taskStatusDbEntity);

    public static RecordsCollection<TaskStatusDto> ToDto(this ICollection<TaskStatusDbEntity>? taskStatusDbEntities) 
        => taskStatusDbEntities == null
            ? RecordsCollection<TaskStatusDto>.Empty
            : Mapper.Map<ICollection<TaskStatusDbEntity>, RecordsCollection<TaskStatusDto>>(taskStatusDbEntities);
}