import React from 'react';
import {useAppSelector} from '../../hooks';
import jwtDecode from 'jwt-decode';
import {DecodeToken} from '../../types/authTypes';
import {useNavigate} from 'react-router-dom';
import {Paper, Typography} from '@mui/material';

const SettingsPage: React.FC = () => {
  const {currentSession} = useAppSelector((x) => x.authReducer);
  const navigate = useNavigate();

  const decodeToken = jwtDecode<DecodeToken>(currentSession!.accessToken);
  if (decodeToken.role != 'Admin') {
    navigate('/');
  }

  return <Paper
    sx={{
      p: 2,
      display: 'flex',
      flexDirection: 'column',
      height: '200vh',
    }}
  >
    <Typography variant="h3" gutterBottom component="div">
      There will be settings page
    </Typography>

  </Paper>;
};

export default SettingsPage;
