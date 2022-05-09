export interface Session {
    accessToken: string,
    refreshToken: string,
    userGuid: string
}

export interface SignIn {
    email: string,
    password: string
}