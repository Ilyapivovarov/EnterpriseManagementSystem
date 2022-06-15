using AutoMapper;
using TaskService.Infrastructure.Mapper.Converters;

namespace TaskService.Infrastructure.Mapper;

public static class MapperExtensions
{
    public static TaskInfo ToDto(this TaskDbEntity taskDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<TaskDbEntity, TaskInfo>()
                .ConvertUsing<TaskDbEntityToTaskInfoConverter>());

        var mapper = new AutoMapper.Mapper(cfg);

        return mapper.Map<TaskDbEntity, TaskInfo>(taskDbEntity);
    }

    public static TaskInfo[] ToDto(this ICollection<TaskDbEntity> taskDbEntity)
    {
        var cfg = new MapperConfiguration(cfg =>
            cfg.CreateMap<TaskDbEntity, TaskInfo>()
                .ConvertUsing<TaskDbEntityToTaskInfoConverter>());

        var mapper = new AutoMapper.Mapper(cfg);

        return mapper.Map<ICollection<TaskDbEntity>, ICollection<TaskInfo>>(taskDbEntity)
            .ToArray();
    }

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