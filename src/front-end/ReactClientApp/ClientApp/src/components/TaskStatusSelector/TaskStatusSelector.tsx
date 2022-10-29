import React, {FC, useState} from 'react';
import {Box, FormControl, InputLabel, MenuItem, Select, Tooltip} from '@mui/material';
import {useGetTaskStatusesQuery} from '../../api/taskStatusApi';
import Loader from '../Loader/Loader';
import {TaskStatusDto} from '../../types/taskTypes';

interface TaskStatusSelectorProps {
  status?: TaskStatusDto,
  onChange: (statusId: TaskStatusDto) => void
}

const TaskStatusSelector: FC<TaskStatusSelectorProps> = ({status, onChange}) => {
  const {isLoading, data, isSuccess, error} = useGetTaskStatusesQuery();
  const [selectedValue, setSelectedValue] = useState<number>(status ? status.id : 1);

  const onClickHandle = (value: TaskStatusDto) => {
    if (value.id != selectedValue) {
      setSelectedValue(value.id);
      onChange(value);
    }
  };

  if (isLoading) {
    return <Loader/>;
  }
  if (isSuccess) {
    return (
      <Box p={1} sx={{minWidth: 120}}>
        <FormControl fullWidth variant="standard">
          <InputLabel id="task-status-select">Status</InputLabel>
          <Tooltip title={'Change status'} placement="top">
            <Select
              labelId="task-status-select"
              id="select-status"
              value={selectedValue ? selectedValue : ''}
            >
              {data.map((x) => <MenuItem
                key={x.id}
                value={x.id}
                onClick={() => onClickHandle(x)}
                disabled={selectedValue == x.id}
              >
                {x.name}
              </MenuItem>)}
            </Select>
          </Tooltip>
        </FormControl>
      </Box>
    );
  }

  return <>{error}</>;
};

export default TaskStatusSelector;
