import React from 'react';
import {TaskDto} from '../../types/taskTypes';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import TaskList from '../../components/TaskList/TaskList';


const taskDtos: TaskDto[] = [
  {
    id: 1,
    guid: 'e7643e99-be07-4c6d-9928-d245420ef451',
    name: 'Test task',
    description: null,
    created: new Date(),
    author: {
      id: 1,
      guid: '609dad81-bb90-44db-bd99-ff5a48bb2de4',
      firstName: 'Admin',
      lastName: 'Admin',
      emailAddress: 'admin@admin.com',
    },
    executor: {
      id: 1,
      guid: '609dad81-bb90-44db-bd99-ff5a48bb2de4',
      firstName: 'Admin',
      lastName: 'Admin',
      emailAddress: 'admin@admin.com',
    },
    inspector: null,
    status: {
      id: 1,
      guid: '8d502153-805e-44ca-b58b-288e1064c1ca',
      name: 'Registered',
    },
  },
];

const TaskListPage: React.FC = () => {
  return (
    <PageWrapper>
      <TaskList tasks={taskDtos} />
    </PageWrapper>
  );
};

export default TaskListPage;
