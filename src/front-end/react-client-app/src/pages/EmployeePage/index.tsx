import React from 'react';
import Loader from '../../components/Loader/Loader';
import EmployeePageContent from '../../components/EmployeePageContent/EmployeePageContent';
import {useParams} from 'react-router-dom';
import {useGetEmployeeByIdQuery} from '../../api/employeeApi';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import {Breadcrumbs, Paper, Typography} from '@mui/material';
import Link from '../../components/Link/Link';

const UserPage: React.FC = () => {
  const {id} = useParams<string>();
  const {data, isLoading, isSuccess, error} = useGetEmployeeByIdQuery(id!);

  if (isLoading) {
    return <Loader/>;
  }

  if (!isSuccess) {
    return <>{JSON.parse(JSON.stringify(error)).data}</>;
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
          <Link to={'/employees'}>
            Employees
          </Link>
          <Typography color="text.primary">{data.user.firstName} {data.user.lastName}</Typography>
        </Breadcrumbs>
      </Paper>
      <PageWrapper>
        <EmployeePageContent employee={data}/>
      </PageWrapper>
    </>
  );
};

export default UserPage;
