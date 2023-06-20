using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EnterpriseManagementSystem.Logging.Abstractions;

public interface IDbLogger<out T> : ILogger<T>
{
    
}