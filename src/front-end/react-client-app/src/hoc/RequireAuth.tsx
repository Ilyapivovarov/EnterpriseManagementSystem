import React from 'react'
import { useNavigate } from 'react-router-dom'
import { useAppSelector } from '../hooks'

const RequireAuth: React.FC = (props) => {
  const navigate = useNavigate()
  const { currentSession } = useAppSelector(x => x.authReducer)

  React.useEffect(() => {
    if (!currentSession) {
      navigate('sign-in')
    }
  }, [currentSession])

  if (currentSession) {
    return (
      <>
        {props.children}
      </>
    )
  }

  return <></>
}

export default RequireAuth
