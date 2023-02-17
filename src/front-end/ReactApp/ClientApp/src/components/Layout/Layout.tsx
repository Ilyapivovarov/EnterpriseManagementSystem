import React from 'react';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Badge from '@mui/material/Badge';
import Container from '@mui/material/Container';
import NotificationsIcon from '@mui/icons-material/Notifications';
import {NavLink, Outlet, useNavigate} from 'react-router-dom';
import LogoutIcon from '@mui/icons-material/Logout';
import {useAppDispatch, useAppSelector} from '../../hooks';
import {signOut} from '../../store/AuthReducer/AuthActionCreators';
import Loader from '../Loader/Loader';
import {AppBar, Button, ButtonGroup, Drawer} from '@mui/material';
import List from '@mui/material/List';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import PeopleIcon from '@mui/icons-material/People';
import ListItemText from '@mui/material/ListItemText';
import {TranslationKeys} from '../../i18n';
import ListAltIcon from '@mui/icons-material/ListAlt';
import AdminNavItems from '../AdminNavItems/AdminNavItems';
import {useTranslation} from 'react-i18next';

const drawerWidth: number = 240;

const Layout: React.FC = () => {
  const dispatch = useAppDispatch();
  const {t} = useTranslation();
  const navigate = useNavigate();
  const {isLoading} = useAppSelector((x) => x.authReducer);


  const logOutHandler = () => {
    dispatch(signOut())
        .unwrap()
        .finally(() => navigate('/sign-in'));
  };

  return (
    <Box sx={{display: 'flex'}}>
      <CssBaseline />
      <AppBar position="fixed" sx={{width: `calc(100% - ${drawerWidth}px)`, ml: `${drawerWidth}px`}}>
        <Toolbar sx={{display: 'flex', alignItems: 'center', justifyContent: 'flex-end'}}>
          <ButtonGroup variant="text" size={'large'} aria-label="text button group" >
            <Button color="inherit" sx={{marginRight: 2}}>
              <Badge badgeContent={4} color="secondary">
                <NotificationsIcon/>
              </Badge>
            </Button>
            <Button color="inherit" onClick={logOutHandler}>
              <Badge color="secondary">
                <LogoutIcon/>
              </Badge>
            </Button>
          </ButtonGroup>
        </Toolbar>
      </AppBar>
      <Drawer sx={{
        'width': drawerWidth,
        'flexShrink': 0,
        '& .MuiDrawer-paper': {
          width: drawerWidth,
          boxSizing: 'border-box',
        },
      }} variant="permanent" anchor="left">
        <Toolbar sx={{display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
          <Typography component="h1" align={'center'} color="inherit" >
            Enterprise{'\n'} Management System
          </Typography>
        </Toolbar>
        <Divider/>
        <List component="nav">
          <ListItemButton component={NavLink} to={'/employees'}>
            <ListItemIcon>
              <PeopleIcon/>
            </ListItemIcon>
            <ListItemText primary={t(TranslationKeys.navMenu.employees)}/>
          </ListItemButton>
          <ListItemButton component={NavLink} to={'/tasks'}>
            <ListItemIcon>
              <ListAltIcon/>
            </ListItemIcon>
            <ListItemText primary={t(TranslationKeys.navMenu.tasks)}>
            </ListItemText>
          </ListItemButton>
          <AdminNavItems/>
        </List>
      </Drawer>
      {isLoading ?
            <Loader/> :
            <Box component="main" sx={{flexGrow: 1, height: '100vh', overflow: 'auto'}}>
              <Toolbar/>
              <Container maxWidth="lg" sx={{mt: 4, mb: 4}}>
                <Outlet/>
              </Container>
            </Box>
      }
    </Box>
  );
};

export default Layout;
