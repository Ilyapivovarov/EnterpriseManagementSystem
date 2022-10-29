import React from 'react';
import {useGetEmployeesByPageQuery} from '../../api/employeeApi';
import Loader from '../../components/Loader/Loader';
import {Breadcrumbs, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import EmployeeListPageContent from '../../components/EmployeeListPageContent/EmployeeListPageContent';
import ErrorView from '../../components/ErrorView/ErrorView';

const EmployeeListPage: React.FC = () => {
  const {isLoading, isSuccess, data} = useGetEmployeesByPageQuery({pageNumber: 1, pageSize: 100});
  return (
    <>
      <Paper
        sx={{p: 2, marginTop: '10px'}}
        elevation={1}>
        <Breadcrumbs aria-label="breadcrumb">
          <Link to={'/'}>Home</Link>
          <Typography color="text.primary">Employees</Typography>
        </Breadcrumbs>
      </Paper>
      <PageWrapper>
        <>
          {isLoading ?? <Loader/>}
          {isSuccess ?
                        <EmployeeListPageContent data={data}/> :
                        <ErrorView errorMessage={'Error while fetching employees'}/>}
        </>
      </PageWrapper>
    </>
  );
};

export default EmployeeListPage;
