using EnterpriseManagementSystem.BusinessModels;
using IdentityService.Core.DbEntities;

namespace IdentityService.Application.Repositories;

public interface IUserRepository
{
   /// <summary>
   /// Get single user.
   /// </summary>
   /// <param name="predicate"></param>
   /// <returns></returns>
   public Task<UserDbEntity> GetUser(Func<UserDbEntity, bool>? predicate = null);
   
   /// <summary>
   /// Get single user.
   /// </summary>
   /// <param name="predicate"></param>
   /// <returns></returns>
   public Task<UserDbEntity?> GetUserOrDefault(Func<UserDbEntity, bool>? predicate = null);

   /// <summary>
   /// Get users array.
   /// </summary>
   /// <param name="predicate"></param>
   /// <returns></returns>
   public Task<UserDbEntity?[]> GetUsers(Func<UserDbEntity, bool>? predicate = null);

   /// <summary>
   /// Update user.
   /// </summary>
   /// <param name="userDbEntities"></param>
   /// <returns></returns>
   public Task Save(params UserDbEntity[] userDbEntities);
}