import React, {useEffect, useState} from 'react';
import {Box, Breadcrumbs, Button, ButtonGroup, FormControl, InputLabel, MenuItem, Paper, Tooltip, Select, Typography} from '@mui/material';
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
import ExecutorSelector from '../../components/ExecutorSelector/ExecutorSelector';
import {TaskDto, TaskStatusDto} from '../../types/taskTypes';
import TaskStatusSelector from '../../components/TaskStatusSelector/TaskStatusSelect';

interface TaskPageContentProps {
  task: TaskDto,
  statuses: TaskStatusDto[],
}

const TaskPageContent: React.FC<TaskPageContentProps> = ({task, statuses}) => {
  const dispatch = useAppDispatch();

  const [updateTaskStatus] = useUpdateTaskStatusMutation();
  const [selectedValue, setSelectedValue] = useState<number>(task.status.id);
  const [isEditMode, setEditMode] = useState<boolean>(false);

  const onClickHandle = async (value: number) => {
    if (value != selectedValue) {
      setSelectedValue(value);
      await updateTaskStatus({taskId: task.id, statusId: value});
      dispatch(showNotification('Status has been changed'));
    }
  };

  const onSave = () => {
    setEditMode(false);
  };

  const onBack = () => {
    setEditMode(false);
  };

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
                <ExecutorSelector currentExecutor={task.executor}/>
                <TaskStatusSelector selectedStatusId={task.status.id}/>
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
};

const TaskPage: React.FC = () => {
  const {id} = useParams();
  const taskId = Number.parseInt(id!);
  const {data: task, isLoading: isTaskLoading, isSuccess: taskLoadingSuccess} = useGetTaskByIdQuery(taskId);
  const {data: statuses, isLoading: isStatusesLoading, isSuccess: taskStatusesSucces} = useGetTaskStatusesQuery();

  if (isTaskLoading || isStatusesLoading) {
    return <Loader/>;
  }
  if (taskLoadingSuccess && taskStatusesSucces) {
    return <TaskPageContent task={task} statuses={statuses}/>;
  }

  return <>Error</>;
};

export default TaskPage;
