import React from 'react';
import {useNavigate} from 'react-router-dom';
import TableContainer from '@mui/material/TableContainer';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';
import TableFooter from '@mui/material/TableFooter';
import TablePagination from '@mui/material/TablePagination';
import {useTheme} from '@mui/material/styles';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import LastPageIcon from '@mui/icons-material/LastPage';
import FirstPageIcon from '@mui/icons-material/FirstPage';
import KeyboardArrowRight from '@mui/icons-material/KeyboardArrowRight';
import KeyboardArrowLeft from '@mui/icons-material/KeyboardArrowLeft';
import {TableHead} from '@mui/material';
import {useGetTasksByPageQuery} from '../../services/taskService';
import Loader from '../Loader/Loader';

const TaskList: React.FC = () => {
  const navigate = useNavigate();

  const [page, setPage] = React.useState<number>(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);

  const {data: tasks, error, isSuccess, isLoading} = useGetTasksByPageQuery({pageNumber: page + 1, pageSize: rowsPerPage});

  if (isLoading) {
    return <Loader/>;
  }
  if (isSuccess) {
    console.log(tasks);
    // Avoid a layout jump when reaching the last page with empty rows.
    const emptyRows =
      page > 0 ? Math.max(0, (1 + page) * rowsPerPage - tasks.length) : 0;

    const handleChangePage = (
        event: React.MouseEvent<HTMLButtonElement> | null,
        newPage: number,
    ) => {
      setPage(newPage);
    };

    const handleChangeRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    ) => {
      setRowsPerPage(parseInt(event.target.value, 10));
      setPage(0);
    };

    return (
      <TableContainer>
        <Table stickyHeader aria-label="custom pagination table">
          <TableBody>
            {(rowsPerPage > 0 ?
                tasks.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage) :
                tasks
            ).map((task) => (
              <TableRow key={task.id} hover onClick={() => navigate(`/tasks/${task.id}`)}
                style={{cursor: 'pointer'}} >
                <TableCell component="th" scope="row">
                  {task.name}
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
          <TableFooter component={TableHead} >
            <TableRow >
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
  } else {
    return <>{error}</>;
  }
};

interface TablePaginationActionsProps {
  count: number;
  page: number;
  rowsPerPage: number;
  onPageChange: (
    event: React.MouseEvent<HTMLButtonElement>,
    newPage: number,
  ) => void;
}

function TablePaginationActions(props: TablePaginationActionsProps) {
  const theme = useTheme();
  const {
    count,
    page,
    rowsPerPage,
    onPageChange,
  } = props;

  const handleFirstPageButtonClick = (
      event: React.MouseEvent<HTMLButtonElement>,
  ) => {
    onPageChange(event, 0);
  };

  const handleBackButtonClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    onPageChange(event, page - 1);
  };

  const handleNextButtonClick =
    (event: React.MouseEvent<HTMLButtonElement>) => onPageChange(event, page + 1);

  const handleLastPageButtonClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    onPageChange(event, Math.max(0, Math.ceil(count / rowsPerPage) - 1));
  };

  return (
    <Box sx={{
      flexShrink: 0,
      ml: 2.5,
    }}
    >
      <IconButton
        onClick={handleFirstPageButtonClick}
        disabled={page === 0}
        aria-label="first page"
      >
        {theme.direction === 'rtl' ? <LastPageIcon/> : <FirstPageIcon/>}
      </IconButton>
      <IconButton
        onClick={handleBackButtonClick}
        disabled={page === 0}
        aria-label="previous page"
      >
        {theme.direction === 'rtl' ? <KeyboardArrowRight/> : <KeyboardArrowLeft/>}
      </IconButton>
      <IconButton
        onClick={handleNextButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="next page"
      >
        {theme.direction === 'rtl' ? <KeyboardArrowLeft/> : <KeyboardArrowRight/>}
      </IconButton>
      <IconButton
        onClick={handleLastPageButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="last page"
      >
        {theme.direction === 'rtl' ? <FirstPageIcon/> : <LastPageIcon/>}
      </IconButton>
    </Box>
  );
}

export default TaskList;
