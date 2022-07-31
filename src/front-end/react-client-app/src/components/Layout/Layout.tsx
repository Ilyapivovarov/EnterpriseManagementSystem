import React from 'react'
import { styled } from '@mui/material/styles'
import CssBaseline from '@mui/material/CssBaseline'
import MuiDrawer from '@mui/material/Drawer'
import Box from '@mui/material/Box'
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar'
import Toolbar from '@mui/material/Toolbar'
import Typography from '@mui/material/Typography'
import Divider from '@mui/material/Divider'
import IconButton from '@mui/material/IconButton'
import Badge from '@mui/material/Badge'
import Container from '@mui/material/Container'
import MenuIcon from '@mui/icons-material/Menu'
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft'
import NotificationsIcon from '@mui/icons-material/Notifications'
import { Outlet, useNavigate } from 'react-router-dom'
import LogoutIcon from '@mui/icons-material/Logout'
import { useAppDispatch, useAppSelector } from '../../hooks'
import { signOut } from '../../store/AuthReducer/AuthActionCreators'
import Loader from '../Loader/Loader'
import NavMenu from '../NavMenu/NavMenu'

const drawerWidth: number = 240

interface AppBarProps extends MuiAppBarProps {
  open?: boolean;
}

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({
  theme,
  open
}) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(['width', 'margin'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}))

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
  ({
    theme,
    open
  }) => ({
    '& .MuiDrawer-paper': {
      position: 'relative',
      whiteSpace: 'nowrap',
      width: drawerWidth,
      transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen
      }),
      boxSizing: 'border-box',
      ...(!open && {
        overflowX: 'hidden',
        transition: theme.transitions.create('width', {
          easing: theme.transitions.easing.sharp,
          duration: theme.transitions.duration.leavingScreen
        }),
        width: theme.spacing(7),
        [theme.breakpoints.up('sm')]: {
          width: theme.spacing(9)
        }
      })
    }
  })
)

const DashboardContent: React.FC = () => {
  const dispatch = useAppDispatch()
  const navigate = useNavigate()
  const { isLoading } = useAppSelector(x => x.authReducer)

  const [open, setOpen] = React.useState(true)
  const toggleDrawer = () => {
    setOpen(!open)
  }

  const logOutHandler = () => {
    dispatch(signOut())
      .unwrap()
      .finally(() => navigate('/sign-in'))
  }

  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline/>
      <AppBar position="absolute" open={open}>
        <Toolbar
          sx={{
            pr: '24px'
          }}
        >
          <IconButton
            edge="start"
            color="inherit"
            aria-label="open drawer"
            onClick={toggleDrawer}
            sx={{
              marginRight: '36px',
              ...(open && { display: 'none' })
            }}
          >
            <MenuIcon/>
          </IconButton>
          <Typography
            component="h1"
            variant="h6"
            color="inherit"
            noWrap
            sx={{ flexGrow: 1 }}
          >

          </Typography>
          <IconButton color="inherit">
            <Badge badgeContent={4} color="secondary">
              <NotificationsIcon/>
            </Badge>
          </IconButton>
          <IconButton color="inherit" onClick={logOutHandler}>
            <Badge color="secondary">
              <LogoutIcon/>
            </Badge>
          </IconButton>
        </Toolbar>
      </AppBar>
      <Drawer variant="permanent" open={open}>
        <Toolbar
          sx={{
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'flex-end',
            px: [1]
          }}
        >
          <IconButton onClick={toggleDrawer}>
            <ChevronLeftIcon/>
          </IconButton>
        </Toolbar>
        <Divider/>
        <NavMenu/>
      </Drawer>
      {isLoading
        ? <Loader/>
        : <Box
          component="main"
          sx={{
            flexGrow: 1,
            height: '100vh',
            overflow: 'auto'
          }}
        >
          <Toolbar/>
          <Container maxWidth="lg" sx={{
            mt: 4,
            mb: 4
          }}>

            <Outlet/>

          </Container>
        </Box>
      }
    </Box>
  )
}

export default function Dashboard () {
  return <DashboardContent/>
}
