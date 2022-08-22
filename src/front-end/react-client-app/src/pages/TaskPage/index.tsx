import React from 'react';
import {useGetTaskByIdQuery} from '../../api/taskApi';
import {useParams} from 'react-router-dom';
import Loader from '../../components/Loader/Loader';
import TaskPageContent from '../../components/TaskPageContent/TaskPageContent';

const TaskPage: React.FC = () => {
  const {id} = useParams();
  const taskId = Number.parseInt(id!);
  const {data, isLoading, isSuccess, error} = useGetTaskByIdQuery(taskId);

  if (isLoading) {
    return <Loader/>;
  }
  if (isSuccess) {
    return (<TaskPageContent task={data}/>);
  }

  return <>{JSON.parse(JSON.stringify(error)).data}</>;
};

export default TaskPage;
