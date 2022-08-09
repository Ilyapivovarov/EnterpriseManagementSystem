import React, {useState} from 'react';
import {Box, Breadcrumbs, Button, ButtonGroup, FormControl, InputLabel, MenuItem, Paper, Select, SelectChangeEvent, Tooltip, Typography} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import Link from '../../components/Link/Link';
import {useGetTaskByIdQuery, useUpdateTaskStatusMutation} from '../../services/taskService';
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

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;


const TaskPage: React.FC = () => {
  const {id} = useParams();
  const taskId = Number.parseInt(id!);
  const dispatch = useAppDispatch();

  const MenuProps = {
    PaperProps: {
      onScroll: (e : React.UIEvent<HTMLDivElement, UIEvent>) => {
        console.log(e.currentTarget.scrollTop);
        console.log(e.currentTarget.scrollHeight - e.currentTarget.clientHeight);
        if (e.currentTarget.scrollHeight - e.currentTarget.clientHeight <= e.currentTarget.scrollTop) {
          if (executors.length < 20) {
            setExecutors((s) => [...s, ...['Van Henry',
              'April Tucker',
              'Ralph Hubbard',
              'Omar Alexander',
              'Bradley Wilkerson',
              'Virginia Andrews',
              'Kelly Snyder']]);
          }
        }
      },
      style: {
        maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
        width: 250,
      },
    },
  };

  const names = [
    'Admin Admin',
    'Van Henry',
    'April Tucker',
    'Ralph Hubbard',
    'Omar Alexander',
    'Bradley Wilkerson',
    'Virginia Andrews',
    'Kelly Snyder',
  ];

  const {data: task, isLoading: isTaskLoading, isSuccess: taskLoadingSuccess} = useGetTaskByIdQuery(taskId);
  const {data: statuses, isLoading: isStatusesLoading, isSuccess: taskStatusesSucces} = useGetTaskStatusesQuery();

  const [updateTaskStatus] = useUpdateTaskStatusMutation();
  const [executorName, setExecutorName] = React.useState<string | undefined>(`${task?.executor.firstName} ${task?.executor.lastName}`);
  const [selectedValue, setSelectedValue] = useState<number | undefined>(task?.status.id);
  const [executors, setExecutors] = useState(names);
  const [isEditMode, setEditMode] = useState<boolean>(false);

  React.useEffect(() => {
    if (task) {
      setSelectedValue(task.status.id);
      setExecutorName(`${task.executor.firstName} ${task.executor.lastName}`);
    }
  }, [task]);

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

  const handleChange = (event: SelectChangeEvent<typeof executorName>) => {
    const {target: {value}} = event;
    setExecutorName(value);
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
                  <FormControl variant="standard" sx={{m: 1, minWidth: 120}}>
                    <InputLabel id="task-executor-selector-lable">Executor</InputLabel>
                    <Select
                      labelId={'task-executor-selector-lable'}
                      variant="standard"
                      id="task-executor-selector"
                      multiline
                      value={executorName}
                      onChange={handleChange}
                      MenuProps={MenuProps}
                    >
                      {executors.map((name, key) => (
                        <MenuItem key={key} value={name}>
                          {name}
                        </MenuItem>
                      ))}
                    </Select>
                  </FormControl>
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
