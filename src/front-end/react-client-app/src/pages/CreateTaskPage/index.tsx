import React from 'react';
import {Box, Breadcrumbs, Button, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import SaveIcon from '@mui/icons-material/Save';
import EditableTextField from '../../components/EditableTextField/EditableTextField';
import TaskUserSelector from '../../components/TaskUserSelector/TaskUserSelector';
import TaskStatusSelector from '../../components/TaskStatusSelector/TaskStatusSelect';
import {useAppDispatch} from '../../hooks';
import {TaskStatusDto, UserDto} from '../../types/taskTypes';

interface CreateTaskPageProps {
}

const CreateTaskPage: React.FC<CreateTaskPageProps> = (props) => {
  const dispatch = useAppDispatch();

  const onSaveHandler = () => {
    console.log('save task');
  };

  const onExecutorChanged = (executor: UserDto) => {
    console.log(executor);
  };

  const onStatusChanged = (status: TaskStatusDto) => {
    console.log(status);
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
            <Button onClick={onSaveHandler} variant="outlined" startIcon={< SaveIcon />}>
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
                  id={'task-name'}
                  lable={'Name'}
                  isEditable={true}
                  fullWidth={false}
                  multiline={false}
                  variant={'outlined'}
                />

              </Typography>
              <Box display={'flex'} justifyContent={'space-between'}>
                <TaskUserSelector lable={'Executor'} onChange={onExecutorChanged}/>
                <TaskStatusSelector onChange={onStatusChanged}/>
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
