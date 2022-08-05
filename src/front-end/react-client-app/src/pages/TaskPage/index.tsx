import React from 'react';
import {Box, Button, ButtonGroup, Typography} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import Link from '../../components/Link/Link';
import {useGetTaskByIdQuery} from '../../services/taskService';
import {useParams} from 'react-router-dom';
import TaskStatusSelector from '../../components/TaskStatusSelector/TaskStatusSelect';
import Loader from '../../components/Loader/Loader';
import ExecutorSelector from '../../components/ExecutorSelector/ExecutorSelector';
import PageWrapper from '../../components/PageWrapper/PageWrapper';

const TaskPage: React.FC = () => {
  const {id} = useParams();
  const {data, isLoading, isSuccess, error} = useGetTaskByIdQuery(id!);

  if (isLoading) {
    return <Loader/>;
  }

  if (isSuccess) {
    return (
      <PageWrapper>
        <div>
          <Box padding={1} display={'flex'} justifyContent={'space-between'}>
            <Typography fontSize={14} paddingLeft={1}>
              <b>EMS-{data.id}</b> created by{' '}
              <Link to={`/users/${data.author.guid}`}>
                {data.author.firstName} {data.author.lastName}{' '}
              </Link>
              on {new Date(data.created).toLocaleDateString()} {new Date(data.created).toLocaleTimeString()}
            </Typography>
            <Box>
              <ButtonGroup size="small">
                <Button key="edit">
                  <EditIcon/>
                </Button>
                <Button key="delete">
                  <DeleteIcon/>
                </Button>
              </ButtonGroup>
            </Box>
          </Box>
          <Box padding={1}>
            <Box display={'flex'} justifyContent={'space-between'}>
              <Typography
                paddingBottom={2}
                paddingTop={1}
                variant="h3"
                paddingLeft={1}
              >
                {data.name}
              </Typography>
              <Box display={'flex'} justifyContent={'space-between'}>
                <div style={{marginRight: '5px'}}>
                  <ExecutorSelector currentExecutor={data.executor}/>
                </div>
                <TaskStatusSelector selectedStatusId={data.status.id}/>
              </Box>
            </Box>
            <Typography fontSize={20} paddingLeft={1}>
              {data.description}
            </Typography>
          </Box>
        </div>
      </PageWrapper>
    );
  } else {
    return <PageWrapper>{JSON.parse(JSON.stringify(error)).data}</PageWrapper>;
  }
};

export default TaskPage;
