import React from 'react'
import { Outlet, useNavigate } from 'react-router-dom'
import { useAppSelector } from '../hooks'

const RequireAnonymous: React.FC = () => {
  const { currentSession } = useAppSelector(x => x.authReducer)
  const navigate = useNavigate()

  React.useEffect(() => {
    if (currentSession) {
      navigate('/')
    }
  }, [currentSession])

  return (
    <Outlet/>
  )
}

export default RequireAnonymous
