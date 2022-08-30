import React from 'react';
import {useGetEmployeesByPageQuery} from '../../api/employeeApi';
import Loader from '../../components/Loader/Loader';
import {Breadcrumbs, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import EmployeeListPageContent from '../../components/EmployeeListPageContent/EmployeeListPageContent';
import ErrorView from '../../components/ErrorView/ErrorView';
import {EmployeeDataResponse} from '../../types/accountTypes';

const EmployeeListPage: React.FC = () => {
  const {isLoading, isSuccess} = useGetEmployeesByPageQuery({pageNumber: 1, pageSize: 100});
  const data : EmployeeDataResponse[] = [
    {
      guid: 'asfasfasf',
      user: {

        firstName: 'admin',
        emailAddress: 'asfasf',
        lastName: 'asfasfas',
        IdentityGuid: 'asfasfsaf',
      },
    },
  ];

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
          {data ?
            <EmployeeListPageContent data={data} /> :
            <ErrorView errorMessage={'Error while fetching employees'}/>}
        </>
      </PageWrapper>
    </>
  );
};

export default EmployeeListPage;
