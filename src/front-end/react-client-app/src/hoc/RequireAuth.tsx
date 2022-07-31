import React from 'react'
import { useNavigate } from 'react-router-dom'
import { useAppSelector } from '../hooks'
import Loader from '../components/Loader/Loader'

const RequireAuth: React.FC = (props) => {
  const navigate = useNavigate()
  const {
    currentSession,
    isLoading
  } = useAppSelector(x => x.authReducer)
  
  React.useEffect(() => {
    if (!currentSession) {
      navigate('/sign-in')
    }
  }, [currentSession])

  if (isLoading) {
    return <Loader/>
  }

  if (currentSession) {
    return (
      <>
        {props.children}
      </>
    )
  }

  return <>Error</>
}

export default RequireAuth
