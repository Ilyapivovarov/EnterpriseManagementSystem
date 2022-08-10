import React, {FC, useState} from 'react';
import {FormControl, InputLabel, MenuItem, Select, Tooltip} from '@mui/material';
import {useGetTaskStatusesQuery} from '../../services/taskStatusesServices';
import Loader from '../Loader/Loader';
import {useUpdateTaskStatusMutation} from '../../services/taskService';
import {useAppDispatch} from '../../hooks';
import {showNotification} from '../../store/NotificationReduser/notificationReduser';

interface TaskStatusSelectorProps {
    selectedStatusId: number
}

const TaskStatusSelector: FC<TaskStatusSelectorProps> = ({selectedStatusId}) => {
  const dispatch = useAppDispatch();

  const {isLoading, data, isSuccess, error} = useGetTaskStatusesQuery();
  const [updateTaskStatus] = useUpdateTaskStatusMutation();


  const [selectedValue, setSelectedValue] = useState<number>(selectedStatusId);
  React.useEffect(() => {
    setSelectedValue(selectedStatusId);
  }, [selectedStatusId]);


  const onClickHandle = async (value: number) => {
    if (value != selectedValue) {
      setSelectedValue(value);
      await updateTaskStatus({taskId: 1, statusId: value});
      dispatch(showNotification('Status has been changed'));
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
