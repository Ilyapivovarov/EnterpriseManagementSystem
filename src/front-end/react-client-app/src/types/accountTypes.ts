export interface Account {
    email?: string
    firstName?: string,
    lastName?: string,
}

export interface EmployeeDataResponse {
    guid: string,
    user: UserDataResponse,
    position?: PositionDataResponse
}

interface UserDataResponse {
    IdentityGuid: string,
    firstName: string,
    lastName: string,
    emailAddress: string,
    dataBrith?: Date
}

interface PositionDataResponse {
    name: string
}
