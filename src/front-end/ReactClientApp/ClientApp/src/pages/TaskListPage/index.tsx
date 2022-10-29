import React from 'react';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import TaskList from '../../components/TaskList/TaskList';
import {Breadcrumbs, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';
import {useNavigate} from 'react-router-dom';
import {useGetTasksByPageQuery} from '../../api/taskApi';
import Loader from '../../components/Loader/Loader';

const TaskListPage: React.FC = () => {
  const navigate = useNavigate();

  const [page, setPage] = React.useState<number>(1);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);

  const {data, error, isSuccess, isLoading} = useGetTasksByPageQuery({pageNumber: page, pageSize: rowsPerPage});

  if (isLoading) {
    return <Loader/>;
  }

  if (isSuccess) {
    console.log(data);
    return (
      <>
        <Paper
          sx={{
            p: 2,
            marginTop: '10px',
          }}
          elevation={1}>
          <Breadcrumbs aria-label="breadcrumb">
            <Link to={'/'}>
                            Home
            </Link>
            <Typography color="text.primary">Tasks</Typography>
          </Breadcrumbs>
        </Paper>

        <PageWrapper>
          <TaskList tasks={data}/>
        </PageWrapper>
      </>
    );
  }

  return <>{JSON.parse(JSON.stringify(error)).data}</>;
};

export default TaskListPage;
