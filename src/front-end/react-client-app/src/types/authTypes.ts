interface Session {
    accessToken: string,
    refreshToken: string,
    userGuid: string
}

interface SignIn {
    email: string,
    password: string
}