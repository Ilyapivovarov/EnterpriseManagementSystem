import React from 'react'
import { useNavigate } from 'react-router-dom'
import { useAppDispatch, useAppSelector } from '../hooks'
import { resetAuthState } from '../store/AuthReducer/AuthActionCreators'

const RequireAuth: React.FC = (props) => {
  const navigate = useNavigate()
  const { currentSession } = useAppSelector(x => x.authReducer)

  const dispatch = useAppDispatch()

  React.useEffect(() => {
    dispatch(resetAuthState())
      .unwrap()
      .catch(() => {
        navigate('/sign-in')
      })
  }, [])

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
