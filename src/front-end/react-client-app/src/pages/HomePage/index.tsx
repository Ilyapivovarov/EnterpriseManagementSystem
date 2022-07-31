import React from 'react'
import { Box, CircularProgress, Typography } from '@mui/material'
import { useGetEmployeeByGuidQuery } from '../../services/employeeService'
import { useAppSelector } from '../../hooks'
import { NavLink } from 'react-router-dom'
import PageWrapper from '../../components/PageWrapper/PageWrapper'

const HomePage: React.FC = () => {
  const { currentSession } = useAppSelector(x => x.authReducer)
  const {
    data,
    isLoading,
    isSuccess
  } = useGetEmployeeByGuidQuery(currentSession!.userGuid)
  return (
    <PageWrapper>
      {isLoading && <Box sx={{ display: 'flex' }}>
        <CircularProgress/>
      </Box>}
      {isSuccess &&
        <Typography> Welcome {data.user.firstName} <NavLink to={'tasks/1'}>First task</NavLink> </Typography>}
    </PageWrapper>
  )
}

export default HomePage
