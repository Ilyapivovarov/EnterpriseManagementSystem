import React from 'react';
import {Box, Breadcrumbs, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';
import {useGetTaskByIdQuery} from '../../services/taskService';
import {useParams} from 'react-router-dom';
import Loader from '../../components/Loader/Loader';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import Notification from '../../components/Notification/Notification';
import EditableTextField from '../../components/EditableTextField/EditableTextField';
import ExecutorSelector from '../../components/ExecutorSelector/ExecutorSelector';
import {TaskDto} from '../../types/taskTypes';
import TaskStatusSelector from '../../components/TaskStatusSelector/TaskStatusSelect';
import EditableMultilineTextField from '../../components/EditableMultilineTextField/EditableMultilineTextField';

interface TaskPageContentProps {
  task: TaskDto,
}

const TaskPageContent: React.FC<TaskPageContentProps> = ({task}) => {
  return (
    <>
      <Paper
        sx={{
          p: 2,
          marginTop: '10px',
        }}
        elevation={1}>
        <Notification/>
        <Breadcrumbs aria-label="breadcrumb">
          <Link to={'/'}>
             Home
          </Link>
          <Link to={'/tasks'}>
              Tasks
          </Link>
          <Typography color="text.primary">EMS-{task.id}</Typography>
        </Breadcrumbs>
      </Paper>
      <PageWrapper>
        <div>
          <Box padding={1} display={'flex'} justifyContent={'space-between'}>
            <Typography fontSize={14} paddingLeft={1}>
              <b>EMS-{task.id}</b> created by{' '}
              <Link to={`/users/${task.author.guid}`}>
                {task.author.firstName} {task.author.lastName}{' '}
              </Link>
              on {new Date(task.created).toLocaleDateString()} {new Date(task.created).toLocaleTimeString()}
            </Typography>
          </Box>
          <Box padding={1}>
            <Box display={'flex'} justifyContent={'space-between'}>
              <Typography
                paddingBottom={2}
                paddingTop={1}
                variant="h3"
                paddingLeft={1}
              >
                <EditableTextField id={'task-name'} lable={'Task name'}
                  isEditable={true}
                  value={task.name}
                  fullWidth={false}
                  multiline={false}
                  variant={'outlined'}
                />

              </Typography>
              <Box display={'flex'} justifyContent={'space-between'}>
                <ExecutorSelector task={task}/>
                <TaskStatusSelector task={task}/>
              </Box>
            </Box>
            <Typography
              paddingBottom={2}
              paddingTop={1}
              variant="h3"
              paddingLeft={1}
            >
              <EditableMultilineTextField
                id={'task-description'}
                lable={'Task description'}
                placeholder={'Task description is empty'}
                isEditable={true}
                value={task.description}
                fullWidth={true}
                onSaveHandler={(value) => console.log(value)}
              />

            </Typography>
          </Box>
        </div>
      </PageWrapper>
    </>
  );
};

const TaskPage: React.FC = () => {
  const {id} = useParams();
  const taskId = Number.parseInt(id!);
  const {data: task, isLoading: isTaskLoading, isSuccess: taskLoadingSuccess} = useGetTaskByIdQuery(taskId);

  if (isTaskLoading) {
    return <Loader/>;
  }
  if (taskLoadingSuccess) {
    return <TaskPageContent task={task}/>;
  }

  return <>ASFASF</>;
};

export default TaskPage;
