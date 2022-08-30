import React from 'react';
import {useGetEmployeesByPageQuery} from '../../api/employeeApi';
import Loader from '../../components/Loader/Loader';
import {Breadcrumbs, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import EmployeeListPageContent from '../../components/EmployeeListPageContent/EmployeeListPageContent';

const EmployeeListPage: React.FC = () => {
  const {isLoading, isSuccess, data, error, isError} = useGetEmployeesByPageQuery({pageNumber: 1, pageSize: 100});
  return (
    <>
      <Paper
        sx={{
          p: 2,
          marginTop: '10px',
        }}
        elevation={1}>
        <Breadcrumbs aria-label="breadcrumb">
          <Link to={'/'}>
            Home
          </Link>
          <Typography color="text.primary">Employees</Typography>
        </Breadcrumbs>
      </Paper>
      <PageWrapper>
        <>
          {isLoading ?? <Loader/>}
          {isSuccess ?? <EmployeeListPageContent data={data} />}
          {isError ?? <Typography>{error}</Typography>}
        </>
      </PageWrapper>
    </>
  );
};

export default EmployeeListPage;
