import React from 'react';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import DashboardIcon from '@mui/icons-material/Dashboard';
import ListItemText from '@mui/material/ListItemText';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import List from '@mui/material/List';
import AdminNavItems from '../AdminNavItems/AdminNavItems';
import {NavLink} from 'react-router-dom';

const NavMenu: React.FC = () => {
  return (
    <List component="nav">
      <ListItemButton component={NavLink} to={'/users'}>
        <ListItemIcon>
          <DashboardIcon/>
        </ListItemIcon>
        <ListItemText primary="Users"/>
      </ListItemButton>
      <ListItemButton component={NavLink} to={'/tasks'}>
        <ListItemIcon>
          <ShoppingCartIcon/>
        </ListItemIcon>
        <ListItemText primary="Tasks">
        </ListItemText>
      </ListItemButton>
      <AdminNavItems/>
    </List>
  );
};

export default NavMenu;
