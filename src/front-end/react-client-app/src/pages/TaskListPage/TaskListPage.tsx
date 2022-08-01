import React from 'react';
import {useTheme} from '@mui/material/styles';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableFooter from '@mui/material/TableFooter';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import IconButton from '@mui/material/IconButton';
import FirstPageIcon from '@mui/icons-material/FirstPage';
import KeyboardArrowLeft from '@mui/icons-material/KeyboardArrowLeft';
import KeyboardArrowRight from '@mui/icons-material/KeyboardArrowRight';
import LastPageIcon from '@mui/icons-material/LastPage';
import {TaskDto} from '../../types/taskTypes';
import {useNavigate} from 'react-router-dom';

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
    }}>
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

const rows: TaskDto[] = [
  {
    id: 1,
    guid: 'e7643e99-be07-4c6d-9928-d245420ef451',
    name: 'Test task',
    description: null,
    created: new Date(),
    author: {
      id: 1,
      guid: '609dad81-bb90-44db-bd99-ff5a48bb2de4',
      firstName: 'Admin',
      lastName: 'Admin',
      emailAddress: 'admin@admin.com',
    },
    executor: {
      id: 1,
      guid: '609dad81-bb90-44db-bd99-ff5a48bb2de4',
      firstName: 'Admin',
      lastName: 'Admin',
      emailAddress: 'admin@admin.com',
    },
    inspector: null,
    status: {
      id: 1,
      guid: '8d502153-805e-44ca-b58b-288e1064c1ca',
      name: 'Registered',
    },
  },
];

function CustomPaginationActionsTable() {
  const navigate = useNavigate();

  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(5);

  // Avoid a layout jump when reaching the last page with empty rows.
  const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

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
    <TableContainer component={Paper}>
      <Table sx={{minWidth: 500}} aria-label="custom pagination table">
        <TableBody>
          {(rowsPerPage > 0 ?
                            rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage) :
                            rows
          ).map((row) => (
            <TableRow key={row.id} hover onClick={() => navigate(`/tasks/${row.id}`)}>
              <TableCell component="th" scope="row">
                {row.name}
              </TableCell>
              <TableCell style={{width: 160}} align="right">
                {row.status.name}
              </TableCell>
              <TableCell style={{width: 160}} align="right">
                {row.executor.emailAddress}
              </TableCell>
            </TableRow>
          ))}
          {emptyRows > 0 && (
            <TableRow style={{height: 53 * emptyRows}}>
              <TableCell colSpan={6}/>
            </TableRow>
          )}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TablePagination
              rowsPerPageOptions={[5, 10, 25, {
                label: 'All',
                value: -1,
              }]}
              colSpan={3}
              count={rows.length}
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
}

const TaskListPage: React.FC = () => {
  return (
    <CustomPaginationActionsTable/>
  );
};

export default TaskListPage;
