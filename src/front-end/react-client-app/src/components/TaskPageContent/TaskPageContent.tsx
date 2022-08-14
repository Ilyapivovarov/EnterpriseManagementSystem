import {TaskDto} from '../../types/taskTypes';
import React from 'react';
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
import ExecutorSelector from '../ExecutorSelector/ExecutorSelector';
import TaskStatusSelector from '../TaskStatusSelector/TaskStatusSelect';

interface TaskPageContentProps {
  task: TaskDto,
}

const TaskPageContent: React.FC<TaskPageContentProps> = ({task}) => {
  const dispatch = useAppDispatch();

  const [editMode, setEditMode] = React.useState(false);

  const onSaveHandler = () => {
    const taskNameElem = document.getElementById('task-name') as HTMLInputElement;
    const taskDEscriptionElem = document.getElementById('task-description') as HTMLInputElement;

    if (taskNameElem.value != task.name || taskDEscriptionElem.value != task.description) {
      console.log('save task');
      dispatch(showNotification('Task has been updated'));
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
              <EditableTextField
                id={'task-description'}
                lable={'Description'}
                isEditable={editMode}
                value={task.description}
                fullWidth={true}
                multiline={true}
                placeholder={'This is task not have description'}
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
