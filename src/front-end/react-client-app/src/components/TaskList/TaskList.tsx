import React from 'react';
import {TaskDto} from '../../types/taskTypes';
import {Box, Button, Card, CardActionArea, CardContent, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent, Tooltip, Typography} from '@mui/material';
import {useNavigate} from 'react-router-dom';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';

import './TaskList.css';

interface TaskListProps {
    tasks: TaskDto[]
}

const TaskList: React.FC<TaskListProps> = ({tasks}) => {
  const navigate = useNavigate();

  const [selectedExecutor, setSelectedExecutor] = React.useState('All');
  const [filteredTasks, setFilteredTasks] = React.useState(tasks);

  const handleChange = (event: SelectChangeEvent) => {
    const value = event.target.value as string;
    setSelectedExecutor(value);
    console.log(value);
    if (value == 'All') {
      setFilteredTasks(tasks);
    } else if (value == 'No executor') {
      setFilteredTasks(tasks.filter((x) => !x.executor));
    } else {
      setFilteredTasks(tasks.filter((x) => x.executor?.emailAddress == value));
    }
  };

  return (
    <Box>
      <Box style={{padding: '5px', display: 'flex', justifyContent: 'space-between'}}>
        <FormControl size={'small'} style={{width: '200px'}}>
          <InputLabel id="executors-filter">Executor</InputLabel>
          <Select
            labelId="executors-filter"
            value={selectedExecutor}
            label="Executors"
            onChange={handleChange}
          >
            <MenuItem value={'All'}>All</MenuItem>
            <MenuItem value={'No executor'}>No executor</MenuItem>
            {tasks.filter((x) => x.executor).map((task) =>
              <MenuItem key={task.id} value={task.executor!.emailAddress}>
                {task.executor!.emailAddress }
              </MenuItem>)
            }
          </Select>
        </FormControl>
        <Tooltip title="Create new task">
          <Button
            color={'primary'}
            variant={'outlined'}
            onClick={() => navigate('new')}
            endIcon={<AddCircleOutlineIcon />}
          >
            Create new task
          </Button>
        </Tooltip>
      </Box>
      <Box style={{padding: '5px'}}>
        {filteredTasks.map((task) =>
          <Card
            variant="outlined"
            key={task.id}
            style={{
              display: 'flex',
              justifyContent: 'space-between',
              marginTop: '2px',
            }}
          >
            <CardActionArea onClick={() => navigate(`${task.id}`)}>
              <CardContent style={{width: '100%'}} >
                <Box style={{display: 'flex', justifyContent: 'space-between'}}>
                  <Box>
                    <Typography variant="h5" component="div">
                      {task.name}
                    </Typography>
                    <Typography variant="body2">
                      {task.description && task.description.length > 70 ?
                        task.description.slice(0, 60).concat('...') :
                        task.description}
                    </Typography>
                  </Box>
                  <Box >
                    <Typography className={`task-status ${task.status.name.toLowerCase()}-status`} variant="h6" component="div">
                      {task.status.name}
                    </Typography>
                  </Box>
                </Box>
              </CardContent>
            </CardActionArea>
          </Card>)}
      </Box>
    </Box>
  );
};

export default TaskList;
