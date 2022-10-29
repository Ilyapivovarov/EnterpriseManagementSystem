import React from 'react';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import List from '@mui/material/List';
import AdminNavItems from '../AdminNavItems/AdminNavItems';
import {NavLink} from 'react-router-dom';
import PeopleIcon from '@mui/icons-material/People';
import ListAltIcon from '@mui/icons-material/ListAlt';

const NavMenu: React.FC = () => {
  return (
    <List component="nav">
      <ListItemButton component={NavLink} to={'/employees'}>
        <ListItemIcon>
          <PeopleIcon/>
        </ListItemIcon>
        <ListItemText primary="Employees"/>
      </ListItemButton>
      <ListItemButton component={NavLink} to={'/tasks'}>
        <ListItemIcon>
          <ListAltIcon/>
        </ListItemIcon>
        <ListItemText primary="Tasks">
        </ListItemText>
      </ListItemButton>
      <AdminNavItems/>
    </List>
  );
};

export default NavMenu;
