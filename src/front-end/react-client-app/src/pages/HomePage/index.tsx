import React from 'react'
import { Box, CircularProgress, Paper, Typography } from '@mui/material'
import { useGetAccountByGuidQuery } from '../../services/accountService'
import { useAppSelector } from '../../hooks'
import { NavLink } from 'react-router-dom'

const HomePage: React.FC = () => {
  const { currentSession } = useAppSelector(x => x.authReducer)
  const {
    data,
    isLoading,
    isSuccess
  } = useGetAccountByGuidQuery(currentSession!.userGuid)
  return (
    <Paper
      sx={{
        p: 2,
        display: 'flex',
        flexDirection: 'column',
        height: '100%'
      }}
    >
      {isLoading && <Box sx={{ display: 'flex' }}>
        <CircularProgress/>
      </Box>}
      {isSuccess &&
        <Typography> Welcome {data.user.firstName} <NavLink to={'tasks/1'}>First task</NavLink> </Typography>}
    </Paper>
  )
}

export default HomePage
