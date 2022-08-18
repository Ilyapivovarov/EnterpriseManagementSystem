import React from 'react';
import {TaskDto} from '../../types/taskTypes';
import {Box, Button, Card, CardActions, CardContent, Typography} from '@mui/material';

interface TaskListProps {
    tasks: TaskDto[]
}

const TaskList: React.FC<TaskListProps> = ({tasks}) => {
  return (
    <Box>
      <Box display={'flex'} flexDirection={'column'} sx={{height: '500px'}}>
        {tasks.map((task) =>
          <Card variant="outlined" style={{display: 'flex', justifyContent: 'space-between'}}>
            <CardContent>
              <Box style={{display: 'flex', justifyContent: 'space-between'}}>
                <Box>
                  <Typography variant="h5" component="div">
                    {task.name}
                  </Typography>
                  <Typography variant="h5" component="div">
                    {task.status.name}
                  </Typography>
                </Box>
                <Typography variant="body2" color="text.secondary">
                  {task.description?.slice(0, 69)}
                </Typography>
              </Box>
            </CardContent>
            <CardActions>
              <Button size="small">Learn More</Button>
            </CardActions>
          </Card>)}
      </Box>
      <Box>
                Action
      </Box>
    </Box>
  );
};

export default TaskList;
