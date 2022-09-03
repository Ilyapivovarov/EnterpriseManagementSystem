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

            public static string RefreshToken(string refreshToken)
            {
                return $"auth/refresh/{refreshToken}";
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

            public static string GetTaskById(string id)
            {
                return $"{BaseUrl}/{id}";
            }

            public static string GetTaskByPage(string pageNumber, string pageSize)
            {
                return $"{BaseUrl}?pageNumber={pageNumber}&pageSize={pageSize}";
            }

            public static string GetTaskByGuid(string guid)
            {
                return $"{BaseUrl}/{guid}";
            }

            public static string SetTaskStatus()
            {
                return $"{BaseUrl}/status";
            }
            
            public static string SetExecutor()
            {
                return $"{BaseUrl}/executor";
            }
            
            public static string SetInspector()
            {
                return $"{BaseUrl}/inspector";
            }


            public static string UpdateTask()
            {
                return BaseUrl;
            }
            
            public static string CreateTask()
            {
                return BaseUrl;
            }
            
            public static string DeleteTask(int taskId)
            {
                return $"{BaseUrl}/{taskId}";
            }
        }

        public static class TaskStatusesController
        {
            private const string BaseUrl = "TaskStatus";

            public static string GetAll()
            {
                return BaseUrl;
            }
        }

        public static class UserController
        {
            private const string BaseUrl = "user";

            public static string GetUsersByPage(int page, int count)
            {
                return $"{BaseUrl}?page={page}&count={count}";
            }
        }
    }

    public static class UserServiceApi
    {
        public static class Employee
        {
            private const string BaseUrl = "employee";

            public static string GetByIdentityGuid(Guid guid)
            {
                return $"{BaseUrl}/{guid}";
            }
            
            public static string GetByPage(int pageNumber, int pageSize)
            {
                return $"{BaseUrl}?pageNumber={pageNumber}&pageSize={pageSize}";
            }
        }

        public static class User
        {
            private const string BaseUrl = "user";

            public static string GetUserByPage(string pageNumber)
            {
                return $"{BaseUrl}/{pageNumber}";
            }

            public static string UpdateUserInfo()
            {
                return $"{BaseUrl}";
            }
        }
    }
}
