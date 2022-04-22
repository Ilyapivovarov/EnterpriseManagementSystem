namespace ApiGateway.Application;

public static class UrlConfig
{
    public static class IdentityApi
    {
        public static class AuthController
        {
            /// <summary>
            ///     Path to sign in method
            /// </summary>
            public static string SignIn() => "auth/sign-in";

            /// <summary>
            ///     Path to sign up method
            /// </summary>
            public static string SignUp() => "auth/sign-up";

            /// <summary>
            ///     Path to sign out method
            /// </summary>
            public static string SignOut() => "auth/sign-out";
        }
        
        public static class UserController
        {
           
            /// <summary>
            /// Get path to get all user method
            /// </summary>
            /// <param name="page">Number of page</param>
            /// <returns></returns>
            public static string GetAllUser(int page = 0) => $"user?page={page}";

            /// <summary>
            /// Get path to GetUserByGuid method
            /// </summary>
            /// <param name="userGuid">User guid</param>
            /// <returns></returns>
            public static string GetUserByGuid(Guid userGuid) => $"user/{userGuid}";

            /// <summary>
            /// Get path to UpdateUserData method
            /// </summary>
            /// <returns></returns>
            public static string UpdateUserData() => "update";
        }
    }
}