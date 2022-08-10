import React, {useEffect, useState} from 'react';
import {FormControl, InputLabel, MenuItem, Select} from '@mui/material';
import {TaskDto, UserDto, UsersByPageDto} from '../../types/taskTypes';
import {useUpdateTaskExecutorMutation} from '../../services/taskService';
import {useAppDispatch} from '../../hooks';
import {showNotification} from '../../store/NotificationReduser/notificationReduser';
import {useLazyGetExecutorsByPageQuery} from '../../services/executorService';

interface ExecutorSelectorProps {
  task: TaskDto,
}

function unique(executors : UserDto[]) {
  const result : UserDto[] = [];

  for (const executor of executors) {
    if (result.filter((x) => x.id == executor.id).length == 0) {
      result.push(executor);
    }
  }
  return result;
}

const ExecutorSelector: React.FC<ExecutorSelectorProps> = ({task}) => {
  const dispatch = useAppDispatch();

  const [getExecutorsByPage] = useLazyGetExecutorsByPageQuery();
  const [page, setPage] = useState(1);
  const [executors, setExecutors] = useState<UserDto[]>([task.executor]);
  const [updateTaskExecutor] = useUpdateTaskExecutorMutation();
  const [executorId, setExecutorId] = React.useState<number>(task.executor.id);
  const [hasExecutorsFlag, setHasExecutorsFlag] = useState(true);

  const fetchExecutors = () => {
    if (hasExecutorsFlag) {
      getExecutorsByPage({page: page, count: 5})
          .unwrap()
          .then((x) => {
            setHasExecutorsFlag(x.total != executors.length);
            setExecutors((s) => unique([...s, ...x.users]));
          });
    }
  };

  React.useEffect(() => {
    fetchExecutors();
  }, []);

  React.useEffect(() => {
    fetchExecutors();
  }, [page]);

  const handleChange = async (value : number) => {
    if (executorId!= value) {
      setExecutorId(value);
      await updateTaskExecutor({taskId: task.id, executorId: value});
      dispatch(showNotification('Executor has been changed'));
    }
  };

  const onScroll = (e : React.UIEvent<HTMLDivElement, UIEvent>) => {
    if (e.currentTarget.scrollHeight - e.currentTarget.clientHeight == e.currentTarget.scrollTop) {
      setPage((x) => x + 1);
    }
  };

  return (
    <FormControl variant="standard" sx={{m: 1, width: 250}}>
      <InputLabel id="task-executor-selector-lable">Executor</InputLabel>
      <Select
        labelId={'task-executor-selector-lable'}
        variant="standard"
        id="task-executor-selector"
        multiline
        value={executorId}
        MenuProps={
          {style: {maxHeight: 150, width: 250},
            PaperProps: {
              onScroll: onScroll,
            }}
        }
      >
        {executors.map((executor, key) => (
          <MenuItem key={key} value={executor.id} disabled={executor.id == task.executor.id} onClick={() => handleChange(executor.id)}>
            {executor.emailAddress}
          </MenuItem>
        ))}
      </Select>

    </FormControl>
  );
};

export default ExecutorSelector;
