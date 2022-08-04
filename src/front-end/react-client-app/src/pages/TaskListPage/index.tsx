import React from 'react';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import TaskList from '../../components/TaskList/TaskList';
import {Breadcrumbs, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';

const TaskListPage: React.FC = () => {
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
        <TaskList/>
      </PageWrapper>
    </>

  );
};

export default TaskListPage;
