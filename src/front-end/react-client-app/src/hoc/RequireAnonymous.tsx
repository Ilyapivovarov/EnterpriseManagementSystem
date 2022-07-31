import React from 'react'
import { Outlet, useNavigate } from 'react-router-dom'
import { useAppDispatch } from '../hooks'
import { resetAuthState } from '../store/AuthReducer/AuthActionCreators'

const RequireAnonymous: React.FC = () => {
  const navigate = useNavigate()
  const dispatch = useAppDispatch()

  React.useEffect(() => {
    dispatch(resetAuthState())
      .unwrap()
      .then(() => {
        navigate('/')
      })
  }, [])

  return (
    <Outlet/>
  )
}

export default RequireAnonymous
