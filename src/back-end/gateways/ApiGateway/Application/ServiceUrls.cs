namespace ApiGateway.Application;

public static class ServiceUrls
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
                return "user";
            }
        }
    }

    public static class TaskApi
    {
        public static class TaskController
        {
            private const string BaseUrl = "task";

            public static string CreateNewTask()
            {
                return BaseUrl;
            }

            public static string GetTaskByGuid(string guid)
            {
                return $"{BaseUrl}/{guid}";
            }

            public static string UpdateTask()
            {
                return BaseUrl;
            }
        }
    }

    public static class UserServiceApi
    {
        private const string BaseUrl = "user";

        public static string GetUserByPage(string pageNumber)
        {
            return $"{BaseUrl}/{pageNumber}";
        }

        public static string GetByIdentityGuid(string guid)
        {
            return $"{BaseUrl}/{guid}";
        }

        public static string UpdateUserInfo()
        {
            return $"{BaseUrl}";
        }
    }
}