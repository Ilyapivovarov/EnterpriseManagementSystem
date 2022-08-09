import React, {useState} from 'react';
import {FormControl, InputLabel, MenuItem, MenuProps, Select, SelectChangeEvent} from '@mui/material';
import {UserDto, UsersByPageDto} from '../../types/taskTypes';
import {Session} from '../../types/authTypes';
import {useUpdateTaskExecutorMutation} from '../../services/taskService';

const baseUrl = process.env.REACT_APP_API_KEY;

interface ExecutorSelectorProps {
  currentExecutor: UserDto,
  selected: number
  onSelect: (value: number) => void;
}

const fetchExecutors = async (page: number) : Promise<UsersByPageDto> => {
  const response = await fetch(`${baseUrl}/user?page=${page}&count=5`, {
    method: 'GET',
    headers: {
      'content-type': 'application/json;charset=UTF-8',
      'authorization': `bearer ${(JSON.parse(localStorage.getItem('session')!) as Session).accessToken}`,
    },
  });
  const result = await response.json();
  return result as UsersByPageDto;
};

function unique(executors : UserDto[]) {
  const result : UserDto[] = [];

  for (const executor of executors) {
    if (result.filter((x) => x.id == executor.id).length == 0) {
      result.push(executor);
    }
  }
  return result;
}

const ExecutorSelector: React.FC<ExecutorSelectorProps> = ({currentExecutor, selected, onSelect}) => {
  const [executors, setExecutors] = useState<UserDto[]>(unique([currentExecutor]));

  const menuProps = () : Partial<MenuProps> => {
    const [page, setPage] = useState(1);
    React.useEffect(() => {
      fetchExecutors(page)
          .then((x) => setExecutors((s) => unique([...s, ...x.users])));
    }, [page]);

    return {
      PaperProps: {
        onScroll: (e : React.UIEvent<HTMLDivElement, UIEvent>) => {
          if (e.currentTarget.scrollHeight - e.currentTarget.clientHeight == e.currentTarget.scrollTop) {
            setPage((x) => x + 1);
          }
        },
        style: {
          maxHeight: 150,
          width: 250,
        },
      },
    };
  };

  return (
    <FormControl variant="standard" sx={{m: 1, width: 250}}>
      <InputLabel id="task-executor-selector-lable">Executor</InputLabel>
      <Select
        labelId={'task-executor-selector-lable'}
        variant="standard"
        id="task-executor-selector"
        multiline
        defaultValue={currentExecutor.id}
        value={selected}
        MenuProps={menuProps()}
      >
        {executors.map((executor, key) => (
          <MenuItem key={key} value={executor.id} onClick={() => onSelect(executor.id)}>
            {executor.emailAddress}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
};

export default ExecutorSelector;
