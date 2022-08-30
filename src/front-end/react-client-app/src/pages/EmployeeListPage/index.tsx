import React from 'react';
import {useGetEmployeesByPageQuery} from '../../api/employeeApi';
import Loader from '../../components/Loader/Loader';
import {Breadcrumbs, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import EmployeeListPageContent from '../../components/EmployeeListPageContent/EmployeeListPageContent';

const EmployeeListPage: React.FC = () => {
  const {isLoading, isSuccess, data, error} = useGetEmployeesByPageQuery({pageNumber: 1, pageSize: 100});

  if (isLoading) {
    return <Loader/>;
  }

  if (!isSuccess) {
    return <>Error</>;
  }

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
        <EmployeeListPageContent data={data} />
      </PageWrapper>
    </>
  );
};

export default EmployeeListPage;
