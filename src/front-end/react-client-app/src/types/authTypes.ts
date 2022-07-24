export interface Session {
    accessToken: string,
    refreshToken: string,
    userGuid: string
}

export interface SignIn {
    email: string,
    password: string
}

export interface SignUp {
    email: string,
    password: string,
    confirmPassword: string,
    firstName: string,
    lastName: string
}

export interface DecodeToken {
    "email": string,
    "sub": string,
    "role": string,
    "exp": number,
    "iss": string,
}