import React from 'react';
import {FormControl, InputLabel, MenuItem, Select} from '@mui/material';
import {UserDto} from '../../types/taskTypes';
import {useLazyGetExecutorsByPageQuery} from '../../services/executorService';

interface ExecutorSelectorProps {
  executor?: UserDto,
  onChange: (executor: UserDto) => void;
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

const ExecutorSelector: React.FC<ExecutorSelectorProps> = ({executor, onChange}) => {
  const [getExecutorsByPage] = useLazyGetExecutorsByPageQuery();
  const [page, setPage] = React.useState(1);
  const [executors, setExecutors] = React.useState<UserDto[]>(executor ? [executor] : []);

  const [executorId, setExecutorId] = React.useState<number | undefined>(executor?.id);
  const [hasExecutorsFlag, setHasExecutorsFlag] = React.useState(true);

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

  const handleChange = (value : UserDto) => {
    if (executorId != value.id) {
      setExecutorId(value.id);
      onChange(value);
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
        value={executorId ? executorId : ''}
        MenuProps={
          {style: {maxHeight: 150, width: 250},
            PaperProps: {
              onScroll: onScroll,
            }}
        }
      >
        {executors.map((x, key) => (
          <MenuItem
            key={key}
            value={x.id}
            disabled={executorId == x.id}
            onClick={() => handleChange(x)}
          >
            {x.emailAddress}
          </MenuItem>
        ))}
      </Select>

    </FormControl>
  );
};

export default ExecutorSelector;
