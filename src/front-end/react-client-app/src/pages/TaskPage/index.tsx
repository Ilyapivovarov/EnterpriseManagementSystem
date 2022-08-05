import React, {useState} from 'react';
import {Box, Breadcrumbs, Button, ButtonGroup, FormControl, InputLabel, MenuItem, Paper, Select, Tooltip, Typography} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import Link from '../../components/Link/Link';
import {useGetTaskByIdQuery, useUpdateTaskStatusMutation} from '../../services/taskService';
import {useParams} from 'react-router-dom';
import Loader from '../../components/Loader/Loader';
import ExecutorSelector from '../../components/ExecutorSelector/ExecutorSelector';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import {useGetTaskStatusesQuery} from '../../services/taskStatusesServices';

const TaskPage: React.FC = () => {
  const {id} = useParams();
  const taskId = Number.parseInt(id!);

  const {data, isLoading, isSuccess, error} = useGetTaskByIdQuery(taskId);
  const {data: statuses, isSuccess: taskStatusesSucces} = useGetTaskStatusesQuery();

  const [updateTaskStatus] = useUpdateTaskStatusMutation();
  const [selectedValue, setSelectedValue] = useState<number | undefined>(data?.status.id);


  const onClickHandle = async (value: number) => {
    if (value != selectedValue) {
      setSelectedValue(value);
      await updateTaskStatus({taskId, statusId: value}).unwrap();
    }
  };

  if (isLoading) {
    return <Loader/>;
  }

  if (isSuccess && taskStatusesSucces) {
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
            <Typography color="text.primary">EMS-{data.id}</Typography>
          </Breadcrumbs>
        </Paper>
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
                  <FormControl variant="standard" sx={{
                    m: 1,
                    minWidth: 120,
                  }}>
                    <InputLabel id="task-status-select">Status</InputLabel>
                    <Tooltip title={'Change status'} placement="top" disableFocusListener>
                      <Select
                        labelId="task-status-select"
                        id="select-status"
                        value={data.status.id}
                      >
                        {statuses.map((x) => <MenuItem key={x.id} value={x.id}
                          onClick={() => onClickHandle(x.id)}>{x.name}</MenuItem>)}
                      </Select>
                    </Tooltip>
                  </FormControl>
                </Box>
              </Box>
              <Typography fontSize={20} paddingLeft={1}>
                {data.description}
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
