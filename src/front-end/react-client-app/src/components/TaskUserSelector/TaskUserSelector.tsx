import React from 'react';
import {Box, FormControl, InputLabel, MenuItem, Select} from '@mui/material';
import {UserDto} from '../../types/taskTypes';
import {useLazyGetExecutorsByPageQuery} from '../../api/executorApi';

interface TaskUserSelectorProps {
  current?: UserDto,
  onChange: (executor: UserDto) => void;
  fullWidth?: boolean,
  lable: string,
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

const TaskUserSelector: React.FC<TaskUserSelectorProps> = ({current, onChange, lable, fullWidth}) => {
  const [getExecutorsByPage] = useLazyGetExecutorsByPageQuery();
  const [page, setPage] = React.useState(1);
  const [executors, setExecutors] = React.useState<UserDto[]>(current ? [current] : []);

  const [executorId, setExecutorId] = React.useState<number | undefined>(current?.id);
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
    <Box p={1}>
      <FormControl variant="standard" fullWidth={fullWidth} sx={{minWidth: 250}}>
        <InputLabel id="task-executor-selector-lable">{lable}</InputLabel>
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
    </Box>
  );
};

export default TaskUserSelector;
