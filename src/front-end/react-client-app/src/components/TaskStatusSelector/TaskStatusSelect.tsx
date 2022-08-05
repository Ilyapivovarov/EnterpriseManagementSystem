import React, {FC, useState} from 'react';
import {FormControl, InputLabel, MenuItem, Select, Tooltip} from '@mui/material';
import {useGetTaskStatusesQuery} from '../../services/taskStatusesServices';
import Loader from '../Loader/Loader';
import Notification from '../Notification/Notification';

interface TaskStatusSelectorProps {
    selectedStatusId: number
}

const TaskStatusSelector: FC<TaskStatusSelectorProps> = ({selectedStatusId}) => {
  const [show, setShow] = React.useState(false);
  const [selectedValue, setSelectedValue] = useState<number>(selectedStatusId);
  const {isLoading, data, isSuccess, error} = useGetTaskStatusesQuery();

  const onClickHandle = (value: number) => {
    if (value != selectedValue) {
      setSelectedValue(value);
      setShow(true);
    }
  };


  if (isLoading) {
    return <Loader/>;
  }
  if (isSuccess) {
    return (
      <FormControl variant="standard" sx={{
        m: 1,
        minWidth: 120,
      }}>
        <Notification message={'Status has been chanched'} isOpen={show} onClose={() => setShow(false)}/>
        <InputLabel id="task-status-select">Status</InputLabel>
        <Tooltip title={'Change status'} placement="top" disableFocusListener>
          <Select
            labelId="task-status-select"
            id="select-status"
            value={selectedValue}
          >
            {data.map((x) => <MenuItem key={x.id} value={x.id}
              onClick={() => onClickHandle(x.id)}>{x.name}</MenuItem>)}
          </Select>
        </Tooltip>
      </FormControl>
    );
  }

  return <>{error}</>;
};

export default TaskStatusSelector;
