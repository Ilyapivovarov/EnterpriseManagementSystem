import React from 'react'
import List from '@mui/material/List'
import DialogTitle from '@mui/material/DialogTitle'
import Dialog from '@mui/material/Dialog'
import { FormControl, InputLabel, MenuItem, Pagination, Select, Stack, Tooltip } from '@mui/material'
import ListItem from '@mui/material/ListItem'
import ListItemAvatar from '@mui/material/ListItemAvatar'
import Avatar from '@mui/material/Avatar'
import PersonIcon from '@mui/icons-material/Person'
import ListItemText from '@mui/material/ListItemText'
import { blue } from '@mui/material/colors'
import { UserDto } from '../../types/taskTypes'

const emails = ['user01@gmail.com', 'user02@gmail.com', 'user03@gmail.com',
  'user04@gmail.com', 'user05@gmail.com', 'user06@gmail.com',
  'user07@gmail.com', 'user08@gmail.com', 'user09@gmail.com',
  'user10@gmail.com', 'user11@gmail.com']

interface UserSelectDialogItemsProps {
  id: number,
  email: string,
  handleListItemClick: Function,
  currentExecutor?: boolean
}

const UserSelectDialogItems: React.FC<UserSelectDialogItemsProps> = ({
  id,
  handleListItemClick,
  email,
  currentExecutor
}) => {
  return (
    <ListItem button onClick={() => handleListItemClick(email)} key={id} disabled={currentExecutor}>
      <ListItemAvatar>
        {currentExecutor ?
          <Avatar sx={{
            bgcolor: blue[100],
            color: blue[600]
          }}>
            <PersonIcon/>
          </Avatar> :
          <Avatar>
            <PersonIcon/>
          </Avatar>}
      </ListItemAvatar>
      <ListItemText primary={email}/>
    </ListItem>
  )
}

interface UserSelectDialogProps {
  open: boolean;
  selectedValue: string;
  onClose: Function;
  currentExecutor?: number
}

const UserSelectDialog: React.FC<UserSelectDialogProps> = ({
  onClose,
  selectedValue,
  open,
  currentExecutor
}) => {
  const handleClose = () => {
    onClose(selectedValue)
  }

  const handleListItemClick = (value: string) => {
    onClose(value)
  }

  const [page, setPage] = React.useState(1)
  const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
    setPage(value)
  }
  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>Set executor</DialogTitle>
      <List sx={{ pt: 0 }}>
        {
          emails.slice(page * 10 - 10, page * 10 - 1)
            .map((email, id) => <UserSelectDialogItems email={email}
                                                       id={id}
                                                       handleListItemClick={() => handleListItemClick(email)}
                                                       key={id}
                                                       currentExecutor={currentExecutor == id}/>)
        }
        <Stack spacing={2}>
          <Pagination count={emails.length % 10 + 1} page={page} onChange={handleChange}/>
        </Stack>
      </List>
    </Dialog>
  )
}

interface UserSelectorProps {
  currentExecutor?: UserDto
}

const UserSelector: React.FC<UserSelectorProps> = ({ currentExecutor }) => {
  const [open, setOpen] = React.useState(false)
  const [selectedValue, setSelectedValue] = React.useState<string>(currentExecutor ?
    currentExecutor.emailAddress
    : '')

  const handleClickOpen = () => {
    setOpen(true)
  }

  const handleClose = (value: string) => {
    setOpen(false)
    setSelectedValue(value)
  }

  return (

    <div>
      <FormControl variant="standard" sx={{
        m: 1,
        minWidth: 200
      }}>
        <InputLabel id="task-executor-select">Executor</InputLabel>
        <Tooltip title={'Change executor'} disableFocusListener>
          <Select labelId="task-executor-select"
                  id="select-executor"
                  value={selectedValue}
                  onClick={handleClickOpen}
                  open={false}>
            <MenuItem value={selectedValue}>
              {selectedValue}
            </MenuItem>
          </Select>
        </Tooltip>
      </FormControl>
      <UserSelectDialog
        selectedValue={selectedValue}
        open={open}
        onClose={handleClose}
        currentExecutor={currentExecutor?.id}
      />
    </div>

  )
}
export default UserSelector
