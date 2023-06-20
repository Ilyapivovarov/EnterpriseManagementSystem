using System.ComponentModel.DataAnnotations;
using EnterpriseManagementSystem.BusinessModels;
using IdentityService.Core.DbEntities.Base;

namespace IdentityService.Core.DbEntities;

public class EmailDbEntity : DbEntityBase
{
    [EmailAddress]
    public EmailAddress Address { get; set; }

    public bool IsVerified { get; set; }
}