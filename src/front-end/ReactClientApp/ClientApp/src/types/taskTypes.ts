export interface UserDto {
    id: number
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
    description?: string,
    created: Date,
    executor?: UserDto,
    inspector?: UserDto,
    author: UserDto,
    status: TaskStatusDto,
}

export interface UsersByPageDto {
    total: number,
    users: UserDto[],
}

export interface CreateTaskDto {
    name: string,
    description?: string,
    authorGuid: string,
    statusId: number,
    executorId?: number,
    inspectorId?: number,
}
