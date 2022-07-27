import React from 'react'
import { useNavigate } from 'react-router-dom'
import { useAppDispatch } from '../hooks'
import { resetAuthState } from '../store/AuthReducer/AuthActionCreators'

const RequireAnonymous: React.FC = ({ children }) => {
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
        <>
            {children}
        </>
    );

};

export default RequireAnonymous;
