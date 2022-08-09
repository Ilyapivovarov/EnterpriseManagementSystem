import React, {useState} from 'react';
import {
  Box,
  Breadcrumbs,
  Button,
  ButtonGroup,
  FormControl,
  InputLabel,
  MenuItem,
  Paper,
  Select,
  SelectChangeEvent,
  Tooltip,
  Typography,
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import Link from '../../components/Link/Link';
import {
  useGetTaskByIdQuery,
  useUpdateTaskExecutorMutation,
  useUpdateTaskStatusMutation,
} from '../../services/taskService';
import {useParams} from 'react-router-dom';
import Loader from '../../components/Loader/Loader';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import {useGetTaskStatusesQuery} from '../../services/taskStatusesServices';
import {useAppDispatch} from '../../hooks';
import {showNotification} from '../../store/NotificationReduser/notificationReduser';
import Notification from '../../components/Notification/Notification';
import EditableTextField from '../../components/EditableTextField/EditableTextField';
import SaveIcon from '@mui/icons-material/Save';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import {red, green, blueGrey} from '@mui/material/colors';
import ExecutorSelector from '../../components/ExecutorSelector/ExecutorSelector';

const TaskPage: React.FC = () => {
  const {id} = useParams();
  const taskId = Number.parseInt(id!);
  const dispatch = useAppDispatch();

  const {data: task, isLoading: isTaskLoading, isSuccess: taskLoadingSuccess} = useGetTaskByIdQuery(taskId);
  const {data: statuses, isLoading: isStatusesLoading, isSuccess: taskStatusesSucces} = useGetTaskStatusesQuery();

  const [updateTaskStatus] = useUpdateTaskStatusMutation();
  const [selectedValue, setSelectedValue] = useState<number | undefined>(task?.status.id);
  const [isEditMode, setEditMode] = useState<boolean>(false);

  const [updateTaskExecutor] = useUpdateTaskExecutorMutation();
  const [executorId, setExecutorId] = React.useState<number>(0);


  React.useEffect(() => {
    if (task) {
      setSelectedValue(task.status.id);
      setExecutorId(task.executor.id);
    }
  }, [task]);

  const handleChange = async (value : number) => {
    if (task?.executor.id != value) {
      await updateTaskExecutor({taskId: 1, executorId: value as number});
      // .unwrap()
      // .then((x) => setExecutorId(value as number));
    }
  };

  const onClickHandle = async (value: number) => {
    if (value != selectedValue) {
      setSelectedValue(value);
      await updateTaskStatus({taskId, statusId: value}).unwrap();
      dispatch(showNotification('Status has been changed'));
    }
  };

  const onSave = () => {
    setEditMode(false);
  };

  const onBack = () => {
    setEditMode(false);
  };


  if (isTaskLoading && isStatusesLoading) {
    return <Loader/>;
  }

  if (taskLoadingSuccess && taskStatusesSucces && selectedValue) {
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
              <Box hidden={isEditMode}>
                <ButtonGroup size="small">
                  <Button key="edit" onClick={() => {
                    setEditMode(!isEditMode);
                    const input = document.getElementById('task-name') as HTMLInputElement | null;
                    console.log(input?.value);
                  }}>
                    <EditIcon/>
                  </Button>
                </ButtonGroup>
              </Box>
              <Box hidden={!isEditMode}>
                <ButtonGroup size="small">
                  <Button key="back" onClick={onBack} sx={{color: blueGrey[500]}}>
                    <ArrowBackIcon/>
                  </Button>
                  <Button key="delete" sx={{color: red[500]}}>
                    <DeleteIcon/>
                  </Button>
                  <Button sx={{color: green[500]}} key="save" onClick={onSave}>
                    <SaveIcon/>
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
                  <EditableTextField id={'task-name'} lable={'Task name'}
                    isEditable={isEditMode}
                    value={task.name}
                    fullWidth={false}
                    multiline={false}
                    variant={'outlined'}
                  />

                </Typography>
                <Box display={'flex'} justifyContent={'space-between'}>
                  <ExecutorSelector currentExecutor={task.executor} selected={executorId} onSelect={handleChange}/>
                  <FormControl variant="standard" sx={{m: 1, minWidth: 120}}>
                    <InputLabel id="task-status-select">Status</InputLabel>
                    <Tooltip title={'Change status'} placement="top" disableFocusListener>
                      <Select
                        labelId="task-status-select"
                        id="select-status"
                        value={selectedValue}
                      >
                        {statuses.map((x) => <MenuItem key={x.id} value={x.id}
                          onClick={() => onClickHandle(x.id)}>{x.name}</MenuItem>)}
                      </Select>
                    </Tooltip>
                  </FormControl>
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
                  lable={'Task description'}
                  isEditable={isEditMode}
                  value={task.description}
                  fullWidth={true}
                  multiline={true}
                  variant={'outlined'}
                />

              </Typography>
            </Box>
          </div>
        </PageWrapper>
      </>
    );
  } else {
    return <PageWrapper>Error</PageWrapper>;
  }
};

export default TaskPage;
