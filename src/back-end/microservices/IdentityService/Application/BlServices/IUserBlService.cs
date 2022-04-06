using IdentityService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.BlServices;

public interface IUserBlService
{
    public User ChngeUserBioInfo(User user, string firstName, string lastName);

    public User ChangeEmail(User user, string email);

    public User ChangePassword(User user, string password);
}