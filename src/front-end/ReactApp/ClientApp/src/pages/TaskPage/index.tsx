import React from 'react';
import {useGetTaskByIdQuery} from '../../api/taskApi';
import {useParams} from 'react-router-dom';
import Loader from '../../components/Loader/Loader';
import TaskPageContent from '../../components/TaskPageContent/TaskPageContent';
import {Breadcrumbs, Paper} from '@mui/material';
import Link from '../../components/Link/Link';

const TaskPage: React.FC = () => {
  const {id} = useParams();
  const taskId = Number.parseInt(id!);
  const {data, isLoading, isSuccess, error} = useGetTaskByIdQuery(taskId);

  if (isLoading) {
    return <Loader/>;
  }
  if (isSuccess) {
    return <>
      <Paper sx={{p: 2, marginTop: '10px'}} elevation={1}>
        <Breadcrumbs aria-label="breadcrumb">
          <Link to={'/'}>
            Home
          </Link>
          <Link to={'/tasks'}>
            Tasks
          </Link>
          <Link to={'#'}>
           #{data.id}
          </Link>
        </Breadcrumbs>
      </Paper>
      <TaskPageContent taskDto={data}/>
    </>;
  }

  return <>{JSON.parse(JSON.stringify(error)).data}</>;
};

export default TaskPage;
