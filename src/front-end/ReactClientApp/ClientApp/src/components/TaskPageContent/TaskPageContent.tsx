import React from 'react';
import {
  useRemoveTaskMutation,
  useSetInspectorMutation,
  useUpdateTaskExecutorMutation,
  useUpdateTaskMutation,
  useUpdateTaskStatusMutation,
} from '../../api/taskApi';
import {TaskDto, TaskStatusDto, UserDto} from '../../types/taskTypes';
import {useAppDispatch} from '../../hooks';
import {showNotification} from '../../store/NotificationReduser/notificationReduser';
import {Box, Breadcrumbs, Button, Paper, Typography} from '@mui/material';
import Link from '../Link/Link';
import PageWrapper from '../PageWrapper/PageWrapper';
import ButtonWithConfirmationWindow from '../ButtonWithConfirmationWindow/ButtonWithConfirmationWindow';
import {Delete, KeyboardBackspace} from '@mui/icons-material';
import SaveIcon from '@mui/icons-material/Save';
import EditIcon from '@mui/icons-material/Edit';
import EditableTextField from '../EditableTextField/EditableTextField';
import TaskUserSelector from '../TaskUserSelector/TaskUserSelector';
import TaskStatusSelector from '../TaskStatusSelector/TaskStatusSelector';
import {useNavigate} from 'react-router-dom';

interface TaskPageContentProps {
  taskDto: TaskDto,
}

const TaskPageContent: React.FC<TaskPageContentProps> = ({taskDto}) => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const [updateTaskExecutor] = useUpdateTaskExecutorMutation();
  const [updateTaskStatus] = useUpdateTaskStatusMutation();
  const [removeTask] = useRemoveTaskMutation();
  const [setInspector] = useSetInspectorMutation();
  const [updateTask] = useUpdateTaskMutation();

  const [task, setTask] = React.useState(taskDto);
  const [isError, setErrorText] = React.useState({isError: false, errorMessage: ''});
  const [editMode, setEditMode] = React.useState(false);

  const onExecutorChanged = (executor: UserDto | null) => {
    updateTaskExecutor({taskId: task.id, executorId: executor?.id})
        .unwrap()
        .then(() => dispatch(showNotification({message: 'Executor has been changed', type: 'success'})))
        .catch(() => dispatch(showNotification({message: 'Error while change executor', type: 'error'})));
  };

  const onStatusChanged = (status: TaskStatusDto) => {
    updateTaskStatus({taskId: task.id, statusId: status.id})
        .unwrap()
        .then(() => dispatch(showNotification({message: 'Status has been changed', type: 'success'})))
        .catch(() => dispatch(showNotification({message: 'Error while change task status', type: 'error'})));
  };

  const onInspectorChanged = (inspector: UserDto | null) => {
    setInspector({taskId: task.id, inspectorId: inspector?.id})
        .unwrap()
        .then(() => dispatch(showNotification({message: 'Inspector has been changed', type: 'success'})))
        .catch(() => dispatch(showNotification({message: 'Error while change inspector', type: 'error'})));
  };

  const onSaveHandler = () => {
    if ((task.name != taskDto.name ||
            task.description != taskDto.description) &&
        (task.name != '')) {
      updateTask({
        id: task.id, guid: task.guid,
        name: task.name, description: task.description,
      })
          .unwrap()
          .then(() => {
            dispatch(showNotification({message: 'Task has been updated', type: 'success'}));
            setEditMode(false);
            setErrorText({isError: false, errorMessage: ''});
          })
          .catch(() => dispatch(showNotification({message: 'Error while update task', type: 'error'})));
    } else {
      setErrorText({isError: true, errorMessage: 'Please, enter name'});
    }
  };

  const onDeleteHandler = () => {
    removeTask(task.id)
        .unwrap()
        .then(() => navigate('/tasks'));
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
              <Link to={`/employees/${task.author.guid}`}>
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
                  errorMessage={isError.errorMessage}
                  error={isError.isError}
                  isEditable={editMode}
                  value={task.name}
                  onChange={(value) => {
                    if (!value) {
                      setErrorText({isError: true, errorMessage: 'Please, enter name'});
                    } else {
                      setErrorText({isError: false, errorMessage: ''});
                    }
                    setTask({...task, name: value ? value : ''});
                  }}
                  fullWidth={false}
                  multiline={false}
                  variant={'outlined'}
                />

              </Typography>
              <Box
                display={'flex'}
                flexDirection={'column'}
                borderLeft={'solid 1px'}
                borderColor={'lightgray'}
              >
                <Box display={'flex'}>
                  <TaskUserSelector
                    current={task.executor}
                    onChange={onExecutorChanged}
                    lable={'Executor'}
                  />
                  <TaskStatusSelector
                    onChange={onStatusChanged}
                    status={task.status}
                  />
                </Box>
                <Box>
                  <TaskUserSelector
                    current={task.inspector}
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
                onChange={(value) => {
                  setTask({...task, description: value ? value : ''});
                }}
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
