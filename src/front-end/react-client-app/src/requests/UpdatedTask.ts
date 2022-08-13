import {TaskStatusDto, UserDto} from '../types/taskTypes';

export interface UpdatedTask{
  id: number,
  guid: string,
  name: string,
  description: string | null,
  created: Date,
  executor: UserDto,
  inspector: UserDto | null,
}
