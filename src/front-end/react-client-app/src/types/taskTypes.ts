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
  executor: UserDto,
  inspectors: UserDto[] | null,
  author: UserDto,
  status: TaskStatusDto,
}

export interface UsersByPageDto {
  total: number,
  users: UserDto[],
}
