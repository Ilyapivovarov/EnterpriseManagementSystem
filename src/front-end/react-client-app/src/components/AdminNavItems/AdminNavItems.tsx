import React, {FC} from 'react';
import ListItemIcon from '@mui/material/ListItemIcon';
import SettingsIcon from '@mui/icons-material/Settings';
import ListItemText from '@mui/material/ListItemText';
import ListItemButton from '@mui/material/ListItemButton';
import {NavLink} from 'react-router-dom';
import {useAppSelector} from '../../hooks';
import jwtDecode from 'jwt-decode';
import {DecodeToken} from '../../types/authTypes';

const AdminNavItems: FC = () => {
  const {currentSession} = useAppSelector((x) => x.authReducer);
  const decodeToken = jwtDecode<DecodeToken>(currentSession!.accessToken);

  if (decodeToken.role == 'Admin') {
    return (
      <ListItemButton component={NavLink} to={'/settings'}>
        <ListItemIcon>
          <SettingsIcon/>
        </ListItemIcon>
        <ListItemText primary="Settings"/>
      </ListItemButton>
    );
  }
  return <></>;
};

export default AdminNavItems;
