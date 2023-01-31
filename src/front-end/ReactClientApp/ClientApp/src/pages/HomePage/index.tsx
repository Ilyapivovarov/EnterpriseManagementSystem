import React from 'react';
import {Box, CircularProgress, Typography} from '@mui/material';
import {useGetEmployeeByGuidQuery} from '../../api/employeeApi';
import {useAppSelector} from '../../hooks';
import {NavLink} from 'react-router-dom';
import PageWrapper from '../../components/PageWrapper/PageWrapper';
import jwtDecode from 'jwt-decode';
import {DecodeToken} from '../../types/authTypes';

const HomePage: React.FC = () => {
  const {currentSession} = useAppSelector((x) => x.authReducer);
  const decodeToken = jwtDecode<DecodeToken>(currentSession!.accessToken);
  const {data, isLoading, isSuccess} = useGetEmployeeByGuidQuery(decodeToken.sub);
  return (
    <PageWrapper>
      {isLoading && <Box sx={{display: 'flex'}}>
        <CircularProgress/>
      </Box>}
      {isSuccess &&
            <Typography> Welcome {data.user.firstName} <NavLink to={'tasks/1'}>First task</NavLink> </Typography>}
    </PageWrapper>
  );
};

export default HomePage;
