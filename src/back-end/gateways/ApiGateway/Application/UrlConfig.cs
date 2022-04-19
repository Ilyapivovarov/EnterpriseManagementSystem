namespace ApiGateway.Application;

public static class UrlConfig
{
    public static class IdentityApi
    {
        /// <summary>
        ///     Path to sign in method
        /// </summary>
        public const string SignIn = "auth/sign-in";

        /// <summary>
        ///     Path to sign up method
        /// </summary>
        public const string SignUp = "auth/sign-up";

        /// <summary>
        ///     Path to test method
        /// </summary>
        public const string Auth = "auth";
    }
}