namespace UserService.Infrastructure.Mapper.Converters;

public sealed class
    EmployeeDbEntityToEmployeeDataResponseConverter : ITypeConverter<EmployeeDbEntity, EmployeeDataResponse>
{
    public EmployeeDataResponse Convert(EmployeeDbEntity source, EmployeeDataResponse destination,
        ResolutionContext context)
    {
        return new EmployeeDataResponse(source.Guid, source.User.ToDto(), source.Position?.ToDto());
    }
}