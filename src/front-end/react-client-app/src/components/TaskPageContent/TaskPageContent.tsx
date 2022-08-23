import React from 'react';
import {useUpdateTaskExecutorMutation, useUpdateTaskMutation, useUpdateTaskStatusMutation} from '../../api/taskApi';
import {TaskDto, TaskStatusDto, UserDto} from '../../types/taskTypes';
import {useAppDispatch} from '../../hooks';
import {showNotification} from '../../store/NotificationReduser/notificationReduser';
import {Box, Breadcrumbs, Button, Paper, Typography} from '@mui/material';
import Link from '../Link/Link';
import PageWrapper from '../PageWrapper/PageWrapper';
import ButtonWithConfirmationWindow from '../ButtonWithConfirmationWindow/ButtonWithConfirmationWindow';
import {Delete, KeyboardBackspace, Login} from '@mui/icons-material';
import SaveIcon from '@mui/icons-material/Save';
import EditIcon from '@mui/icons-material/Edit';
import EditableTextField from '../EditableTextField/EditableTextField';
import TaskUserSelector from '../TaskUserSelector/TaskUserSelector';
import TaskStatusSelector from '../TaskStatusSelector/TaskStatusSelect';

interface TaskPageContentProps {
  task: TaskDto,
}

const TaskPageContent: React.FC<TaskPageContentProps> = ({task}) => {
  const dispatch = useAppDispatch();

  const [updateTaskExecutor] = useUpdateTaskExecutorMutation();
  const [updateTaskStatus] = useUpdateTaskStatusMutation();
  const [updateTask] = useUpdateTaskMutation();

  const [editMode, setEditMode] = React.useState(false);

  const onExecutorChanged = (executor: UserDto) => {
    updateTaskExecutor({taskId: task.id, executorId: executor.id})
        .unwrap()
        .then(() => dispatch(showNotification({message: 'Executor has been changed', type: 'success'})))
        .catch(() => dispatch(showNotification({message: 'Error while change executor', type: 'error'})));
  };

  const onStatusChanged = (status: TaskStatusDto) => {
    updateTaskStatus({taskId: task.id, statusId: status.id})
        .unwrap()
        .then(() => dispatch(showNotification( {message: 'Status has been changed', type: 'success'})))
        .catch(() => dispatch(showNotification( {message: 'Error while change task status', type: 'error'})));
  };

  const onInspectorChanged = (inspector: UserDto) => {
    console.log(inspector);
  };

  const onSaveHandler = () => {
    const taskNameElem = document.getElementById('task-name') as HTMLInputElement;
    const taskDescriptionElem = document.getElementById('task-description') as HTMLInputElement;

    if (taskNameElem.value != task.name || taskDescriptionElem.value != task.description) {
      updateTask({
        id: task.id, guid: task.guid,
        name: taskNameElem.value, description: taskDescriptionElem.value,
      })
          .unwrap()
          .then(() => {
            dispatch(showNotification({message: 'Task has been updated', type: 'success'}));
          })
          .catch(() => dispatch(showNotification({message: 'Error while update task', type: 'error'})));
    }

    setEditMode(false);
  };

  const onDeleteHandler = () => {
    console.log('delete task');
  };

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
            <Box display={'flex'} justifyContent={'space-between'}>
              {editMode ?
                <>
                  <ButtonWithConfirmationWindow
                    title={'Confirm action'}
                    onClickAgree={() => setEditMode(false)}
                    content={'Are you sure you want to go back? This will result in the loss of unsaved changes'}
                  >
                    <KeyboardBackspace/>
                  </ButtonWithConfirmationWindow>
                  <Button onClick={onSaveHandler}>
                    <SaveIcon/>
                  </Button>
                </> :
                <>
                  <Button onClick={() => setEditMode(true)}>
                    <EditIcon/>
                  </Button>
                  <ButtonWithConfirmationWindow
                    onClickAgree={onDeleteHandler}
                    title={'Confirm action '}
                    content={'Do you really want to delete the task?'}
                  >
                    <Delete/>
                  </ButtonWithConfirmationWindow>
                </>}
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
                <EditableTextField
                  id={'task-name'}
                  lable={'Name'}
                  isEditable={editMode}
                  value={task.name}
                  fullWidth={false}
                  multiline={false}
                  variant={'outlined'}
                />

              </Typography>
              <Box display={'flex'} flexDirection={'column'}>
                <Box display={'flex'}>
                  <TaskUserSelector current={task.executor} onChange={onExecutorChanged} lable={'Executor'}/>
                  <TaskStatusSelector onChange={onStatusChanged} status={task.status}/>
                </Box>
                <Box>
                  <TaskUserSelector
                    current={task.executor}
                    onChange={onInspectorChanged}
                    lable={'Inspector'}
                    fullWidth
                  />
                </Box>
              </Box>
            </Box>
            <Typography
              paddingBottom={2}
              paddingTop={1}
              variant="h3"
              paddingLeft={1}
            >
              <EditableTextField
                id={'task-description'}
                lable={'Description'}
                isEditable={editMode}
                value={task.description}
                fullWidth={true}
                multiline={true}
                placeholder={'This task has no description'}
                variant={'outlined'}
              />
            </Typography>
          </Box>
        </div>
      </PageWrapper>
    </>
  );
};

export default TaskPageContent;
