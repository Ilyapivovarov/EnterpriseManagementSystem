import React from 'react';
import TableContainer from '@mui/material/TableContainer';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';
import {TableHead, Typography} from '@mui/material';
import TableFooter from '@mui/material/TableFooter';
import TablePagination from '@mui/material/TablePagination';
import {TaskDto} from '../../types/taskTypes';
import {useNavigate} from 'react-router-dom';

interface TaskListPageProps {
  tasks: TaskDto[]
}

const TaskListPage: React.FC<TaskListPageProps> = ({tasks}) => {
  const navigate = useNavigate();

  return (
    <TableContainer>
      <Table stickyHeader aria-label="custom pagination table">
        <TableBody>
          {tasks.map((task) => (
            <TableRow key={task.id} hover onClick={() => navigate(`/tasks/${task.id}`)}
              style={{cursor: 'pointer'}}>
              <TableCell component="th" scope="row">
                <Typography component={'span'}>
                  <b>EMS-{task.id}</b> {task.name}
                </Typography>
              </TableCell>
              <TableCell style={{width: 160}} align="right">
                {task.status.name}
              </TableCell>
              <TableCell style={{width: 160}} align="right">
                {task.executor.emailAddress}
              </TableCell>
            </TableRow>
          ))}
          {emptyRows > 0 && (
            <TableRow style={{height: 53 * emptyRows}}>
              <TableCell colSpan={6}/>
            </TableRow>
          )}
        </TableBody>
        <TableFooter component={TableHead}>
          <TableRow>
            <TablePagination
              rowsPerPageOptions={[5, 10, 25, {
                label: 'All',
                value: -1,
              }]}
              colSpan={3}
              count={tasks.length}
              rowsPerPage={rowsPerPage}
              page={page}
              SelectProps={{
                inputProps: {
                  'aria-label': 'rows per page',
                },
                native: true,
              }}
              onPageChange={handleChangePage}
              onRowsPerPageChange={handleChangeRowsPerPage}
              ActionsComponent={TablePaginationActions}
            />
          </TableRow>
        </TableFooter>
      </Table>
    </TableContainer>
  );
};

export default TaskListPage;
