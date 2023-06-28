import React from 'react';
import {Box, FormControl, InputLabel, MenuItem, Select} from '@mui/material';
import {useLazyGetEmployeesByPageQuery} from "../../api/employeeApi";
import {EmployeeDataResponse} from "../../types/accountTypes";

interface TaskUserSelectorProps {
  current?: EmployeeDataResponse,
  onChange: (executor: EmployeeDataResponse | null) => void;
  fullWidth?: boolean,
  lable: string,
}

function unique(executors: EmployeeDataResponse[]) {
  const result: EmployeeDataResponse[] = [];
  for (const executor of executors) {
    if (result.filter((x) => x.id == executor.id).length == 0) {
      result.push(executor);
    }
  }
  return result;
}

const TaskUserSelector: React.FC<TaskUserSelectorProps> = ({current, onChange, lable, fullWidth}) => {
  const [getEmployeesByPage] = useLazyGetEmployeesByPageQuery();
  const [page, setPage] = React.useState(1);
  const [executors, setExecutors] = React.useState<EmployeeDataResponse[]>(current ? [current] : []);

  const [executorId, setExecutorId] = React.useState<number>(current ? current.id : 0);
  const [hasExecutorsFlag, setHasExecutorsFlag] = React.useState(true);

  const fetchExecutors = () => {
    if (hasExecutorsFlag) {
      getEmployeesByPage({pageNumber: page, pageSize: 5})
          .unwrap()
          .then((x) => {
            setExecutors((s) => unique([...s, ...x]));
          });
    }
  };

  React.useEffect(() => {
    fetchExecutors();
  }, []);

  React.useEffect(() => {
    fetchExecutors();
  }, [page]);

  const handleChange = (value: EmployeeDataResponse | null) => {
    if (executorId != value?.id) {
      setExecutorId(value ? value.id : 0);
      onChange(value);
    }
  };

  const onScroll = (e: React.UIEvent<HTMLDivElement, UIEvent>) => {
    if (e.currentTarget.scrollHeight - e.currentTarget.clientHeight == e.currentTarget.scrollTop) {
      setPage((x) => x + 1);
    }
  };

  return (
    <Box p={1}>
      <FormControl variant="standard" fullWidth={fullWidth} >
        <InputLabel id="task-executor-selector-lable">{lable}</InputLabel>
        <Select
          labelId={'task-executor-selector-lable'}
          variant="standard"
          id="task-executor-selector"
          multiline
          value={executorId}
          MenuProps={
            {
              style: {maxHeight: 150},
              PaperProps: {
                onScroll: onScroll,
              },
            }
          }
        >
          <MenuItem
            key={'empty'}
            value={0}
            disabled={executorId == 0}
            onClick={() => handleChange(null)}
          >
              Nobody
          </MenuItem>
          {executors.map((x, key) => (
            <MenuItem
              key={key}
              value={x.id}
              disabled={executorId == x.id}
              onClick={() => handleChange(x)}
            >
              {x.user.emailAddress}
            </MenuItem>
          ))}
        </Select>

      </FormControl>
    </Box>
  );
};

export default TaskUserSelector;
