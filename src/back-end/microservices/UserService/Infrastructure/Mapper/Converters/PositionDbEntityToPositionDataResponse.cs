namespace UserService.Infrastructure.Mapper.Converters;

public sealed class PositionDbEntityToPositionDataResponse : ITypeConverter<PositionDbEntity, PositionDataResponse>
{
    public PositionDataResponse Convert(PositionDbEntity source, PositionDataResponse destination,
        ResolutionContext context)
    {
        return new PositionDataResponse(source.Name);
    }
}