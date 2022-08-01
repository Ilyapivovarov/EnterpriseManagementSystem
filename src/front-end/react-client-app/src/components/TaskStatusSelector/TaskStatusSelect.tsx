import React, {FC, useState} from 'react';
import {FormControl, InputLabel, MenuItem, Select, Tooltip} from '@mui/material';
import {useGetTaskStatusesQuery} from '../../services/taskStatusesServices';
import Loader from '../Loader/Loader';

interface TaskStatusSelectorProps {
    selectedStatusId: number
}

const TaskStatusSelector: FC<TaskStatusSelectorProps> = ({selectedStatusId}) => {
  const [selectedValue, setSelectedValue] = useState<number>(selectedStatusId);
  const {
    isLoading,
    data,
    isSuccess,
    error,
  } = useGetTaskStatusesQuery();

  if (isLoading) {
    return <Loader/>;
  }

  if (isSuccess) {
    return (
      <FormControl variant="standard" sx={{
        m: 1,
        minWidth: 120,
      }}>
        <InputLabel id="task-status-select">Status</InputLabel>
        <Tooltip title={'Change status'} placement="top" disableFocusListener>
          <Select
            labelId="task-status-select"
            id="select-status"
            value={selectedValue}
          >
            {data.map((x) => <MenuItem key={x.id} value={x.id}
              onClick={() => setSelectedValue(x.id)}>{x.name}</MenuItem>)}
          </Select>
        </Tooltip>
      </FormControl>
    );
  }

  return <>{error}</>;
};

export default TaskStatusSelector;
