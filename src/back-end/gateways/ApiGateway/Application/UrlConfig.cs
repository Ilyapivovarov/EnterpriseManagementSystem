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
            public static string SignIn()
            {
                return "auth/sign-in";
            }

            /// <summary>
            ///     Path to sign up method
            /// </summary>
            public static string SignUp()
            {
                return "auth/sign-up";
            }

            /// <summary>
            ///     Path to sign out method
            /// </summary>
            public static string SignOut()
            {
                return "auth/sign-out";
            }
        }

        public static class UserController
        {
            /// <summary>
            ///     Get path to get all user method
            /// </summary>
            /// <param name="page">Number of page</param>
            /// <returns></returns>
            public static string GetAllUser(int page = 0)
            {
                return $"user?page={page}";
            }

            /// <summary>
            ///     Get path to GetUserByGuid method
            /// </summary>
            /// <param name="userGuid">User guid</param>
            /// <returns></returns>
            public static string GetUserByGuid(Guid userGuid)
            {
                return $"user/{userGuid}";
            }

            /// <summary>
            ///     Get path to UpdateUserData method
            /// </summary>
            /// <returns></returns>
            public static string UpdateUserData()
            {
                return "update";
            }
        }
    }
}