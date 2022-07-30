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
import { useGetUsersByPageQuery } from '../../services/executorService'

interface ExecutorSelectorDialogItemsProps {
  id: number,
  email: string,
  handleListItemClick: Function,
  isCurrentUser?: boolean
}

const ExecutorSelectorDialogItems: React.FC<ExecutorSelectorDialogItemsProps> = ({
  id,
  handleListItemClick,
  email,
  isCurrentUser
}) => {
  return (
    <ListItem button onClick={() => handleListItemClick(email)} key={id} disabled={isCurrentUser}>
      <ListItemAvatar>
        {isCurrentUser ?
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

interface ExecutorSelectorDialogProps {
  open: boolean;
  selectedValue: string;
  onClose: Function;
  currentExecutor?: number
}

const ExecutorSelectorDialog: React.FC<ExecutorSelectorDialogProps> = ({
  onClose,
  selectedValue,
  open,
  currentExecutor
}) => {
  const handleClose = () => {
    onClose(selectedValue)
  }

  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>Set executor</DialogTitle>
      <ExecutorListWithPag onClose={onClose} currentExecutor={currentExecutor}/>
    </Dialog>
  )
}

interface ExecutorListWithPagProps {
  onClose: Function;
  currentExecutor?: number
}

const ExecutorListWithPag: React.FC<ExecutorListWithPagProps> = ({
  currentExecutor,
  onClose
}) => {

  const pageSize = 10
  const [page, setPage] = React.useState<number>(1)
  const {
    data,
    isSuccess,
    error
  } = useGetUsersByPageQuery({
    page,
    count: pageSize
  })

  const handleListItemClick = (value: string) => {
    onClose(value)
  }

  const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
    setPage(value)
  }

  if (isSuccess) {
    return (
      <List sx={{ pt: 0 }}>
        {
          data.users.map(x => <ExecutorSelectorDialogItems email={x.emailAddress}
                                                           id={x.id}
                                                           handleListItemClick={() => handleListItemClick(x.emailAddress)}
                                                           key={x.id}
                                                           isCurrentUser={currentExecutor == x.id}/>)
        }
        <Stack spacing={2}>
          <Pagination count={Math.ceil(data.total / pageSize)} page={page} onChange={handleChange}/>
        </Stack>
      </List>
    )
  }

  return <>{error}</>
}

interface ExecutorSelectorProps {
  currentExecutor?: UserDto
}

const ExecutorSelector: React.FC<ExecutorSelectorProps> = ({ currentExecutor }) => {
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
      <ExecutorSelectorDialog
        selectedValue={selectedValue}
        open={open}
        onClose={handleClose}
        currentExecutor={currentExecutor?.id}
      />
    </div>

  )
}
export default ExecutorSelector
