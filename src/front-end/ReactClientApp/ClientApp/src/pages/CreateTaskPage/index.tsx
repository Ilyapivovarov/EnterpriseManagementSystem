import React from 'react';
import {Box, Breadcrumbs, Button, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import SaveIcon from '@mui/icons-material/Save';
import EditableTextField from '../../components/EditableTextField/EditableTextField';
import TaskUserSelector from '../../components/TaskUserSelector/TaskUserSelector';
import TaskStatusSelector from '../../components/TaskStatusSelector/TaskStatusSelector';
import {useAppDispatch, useAppSelector} from '../../hooks';
import {TaskStatusDto, UserDto} from '../../types/taskTypes';
import {useCreateTaskMutation} from '../../api/taskApi';
import {useNavigate} from 'react-router-dom';
import {showNotification} from '../../store/NotificationReduser/notificationReduser';
import {CreateTaskModel} from './types';
import jwtDecode from 'jwt-decode';
import {DecodeToken} from '../../types/authTypes';

const CreateTaskPage: React.FC = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const [createTask] = useCreateTaskMutation();
  const {currentSession} = useAppSelector((x) => x.authReducer);
  const decodeToken = jwtDecode<DecodeToken>(currentSession!.accessToken);

  const [isError, setErrorText] = React.useState({isError: false, errorMessage: ''});
  const [task, setTask] = React.useState<CreateTaskModel>({
    authorGuid: decodeToken.sub,
    statusId: 1,
  });

  const onSaveHandler = () => {
    if (task.name && task.statusId) {
      createTask({
        authorGuid: task.authorGuid,
        name: task.name,
        inspectorId: task.inspectorId,
        description: task.description,
        statusId: task.statusId,
        executorId: task.executorId,
      })
          .unwrap()
          .then((x) => navigate(`/tasks/${x}`));
    } else {
      setErrorText({isError: true, errorMessage: 'Please, enter name'});
      dispatch(showNotification({type: 'error', message: 'Please fill in the required fields '}));
    }
  };

  const onExecutorChanged = (executor: UserDto | null) => {
    setTask({...task, executorId: executor?.id});
  };

  const onStatusChanged = (status: TaskStatusDto) => {
    setTask({...task, statusId: status?.id});
  };

  const onInspectorChanged = (inspector: UserDto | null) => {
    setTask({...task, inspectorId: inspector?.id});
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
          <Typography color="text.primary">Create task</Typography>
        </Breadcrumbs>
      </Paper>
      <PageWrapper>
        <div>
          <Box padding={1} display={'flex'} justifyContent={'space-between'}>
            <Typography variant="h5" component="div">
                Creating task
            </Typography>
            <Button onClick={onSaveHandler} variant="outlined" startIcon={< SaveIcon/>}>
                Save
            </Button>
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
                  required
                  error={isError.isError}
                  errorMessage={isError.errorMessage}
                  id={'task-name'}
                  lable={'Name'}
                  onChange={(value) => {
                    if (!value) {
                      setErrorText({isError: true, errorMessage: 'Please, enter name'});
                    } else {
                      setErrorText({isError: false, errorMessage: ''});
                    }
                    setTask({...task, name: value});
                  }}
                  isEditable={true}
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
                    onChange={onExecutorChanged}
                    lable={'Executor'}
                  />
                  <TaskStatusSelector
                    onChange={onStatusChanged}
                  />
                </Box>
                <Box>
                  <TaskUserSelector
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
                onChange={(value) => setTask({...task, description: value})}
                isEditable={true}
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

export default CreateTaskPage;
