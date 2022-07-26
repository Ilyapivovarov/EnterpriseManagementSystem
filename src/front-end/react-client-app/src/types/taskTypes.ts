export interface UserDto {
    guid: string,
    firstName: string,
    lastName: string,
    emailAddress: string,
}

export interface TaskStatusDto {
    id: number,
    guid: string,
    name: string
}

export interface TaskDto {
    id: number,
    guid: string,
    name: string,
    description: string,
    created: Date,
    executor: UserDto,
    observers: UserDto[],
    inspector: UserDto,
    author: UserDto,
    status: TaskStatusDto,
}