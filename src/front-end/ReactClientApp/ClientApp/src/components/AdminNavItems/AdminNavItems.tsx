import React from 'react';
import ListItemIcon from '@mui/material/ListItemIcon';
import SettingsIcon from '@mui/icons-material/Settings';
import ListItemText from '@mui/material/ListItemText';
import ListItemButton from '@mui/material/ListItemButton';
import {NavLink} from 'react-router-dom';
import {useAppSelector} from '../../hooks';
import jwtDecode from 'jwt-decode';
import {DecodeToken} from '../../types/authTypes';
import {useTranslation} from 'react-i18next';
import {TranslationKeys} from '../../i18n';

const AdminNavItems: React.FC = () => {
  const {currentSession} = useAppSelector((x) => x.authReducer);
  const decodeToken = jwtDecode<DecodeToken>(currentSession!.accessToken);
  const {t} = useTranslation();

  if (decodeToken.role == 'Admin') {
    return (
      <ListItemButton component={NavLink} to={'/settings'}>
        <ListItemIcon>
          <SettingsIcon/>
        </ListItemIcon>
        <ListItemText primary={t(TranslationKeys.navMenu.settings)}/>
      </ListItemButton>
    );
  }
  return <></>;
};

export default AdminNavItems;
