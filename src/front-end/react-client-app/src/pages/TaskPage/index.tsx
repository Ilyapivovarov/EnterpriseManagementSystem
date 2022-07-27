import React from 'react'
import { Box, Button, ButtonGroup, Paper, Typography } from '@mui/material'
import EditIcon from '@mui/icons-material/Edit'
import DeleteIcon from '@mui/icons-material/Delete'
import Link from '../../components/Link/Link'
import { TaskDto } from '../../types/taskTypes'
import TaskStatusSelector from '../../components/TaskStatusSelector/TaskStatusSelect'
import UserSelector from '../../components/UserSelector/UserSelector'

const task: TaskDto = {
  id: 1,
  guid: 'ca851edd-cafd-4ce2-9daa-1ee323502f96',
  name: 'Test task',
  description: 'Show task',
  created: new Date(),
  executor: {
    id: 1,
    emailAddress: 'admin@admin.com',
    firstName: 'Admin',
    lastName: 'Admin',
    guid: '036e58dc-17c3-4c60-93a8-572d6089e3f1'
  },
  author: {
    id: 1,
    emailAddress: 'admin@admin.com',
    firstName: 'Admin',
    lastName: 'Admin',
    guid: '036e58dc-17c3-4c60-93a8-572d6089e3f1'
  },

  inspector: {
    id: 1,
    emailAddress: 'admin@admin.com',
    firstName: 'Admin',
    lastName: 'Admin',
    guid: '036e58dc-17c3-4c60-93a8-572d6089e3f1'
  },
  observers: [
    {
      id: 1,
      emailAddress: 'admin@admin.com',
      firstName: 'Admin',
      lastName: 'Admin',
      guid: '036e58dc-17c3-4c60-93a8-572d6089e3f1'
    }
  ],
  status: {
    id: 2,
    name: 'Active',
    guid: '84d8a7a6-c2be-41c4-90e1-36a7969ab3f1',
  }
}

const TaskPage = () => {
  return (
    <Paper
      sx={{
        padding: 2,
        display: 'flex',
        flexDirection: 'column',
        height: '100%',
      }}>
      <Box padding={1} display={'flex'} justifyContent={'space-between'}>
        <Typography fontSize={14} paddingLeft={1}>
          Task-{task.id} created by <Link
          to={`/users/${task.author.guid}`}>{task.author.firstName} {task.author.lastName} </Link>
          {task.created.toLocaleDateString()}
        </Typography>
        <Box>
          <ButtonGroup size="small">
            <Button key="edit"><EditIcon/></Button>
            <Button key="delete"><DeleteIcon/></Button>
          </ButtonGroup>
        </Box>

      </Box>
      <Box padding={1}>
        <Box display={'flex'} justifyContent={'space-between'}>
          <Typography paddingBottom={2} paddingTop={1} variant="h3" paddingLeft={1}>
            {task.name}
          </Typography>
          <Box display={'flex'} justifyContent={'space-between'}>
            <div style={{ marginRight: '5px' }}>
              <UserSelector currentExecutor={task.executor.id}/>
            </div>
            <TaskStatusSelector selectedStatusId={task.status.id}/>
          </Box>
        </Box>
        <Typography fontSize={20} paddingLeft={1}>
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Assumenda, cumque eaque fuga illum in
          molestiae perferendis porro quaerat recusandae soluta. Assumenda molestiae quae reiciendis
          repudiandae soluta? Amet assumenda minima minus!
        </Typography>
      </Box>
    </Paper>)
}

export default TaskPage
