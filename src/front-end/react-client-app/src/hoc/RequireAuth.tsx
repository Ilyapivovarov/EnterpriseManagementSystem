import React from 'react';
import {useNavigate} from 'react-router-dom';
import {useAppSelector} from '../hooks';
import Loader from '../components/Loader/Loader';

const RequireAuth: React.FC = (props) => {
  const navigate = useNavigate();
  const {currentSession, isLoading, isAuth} = useAppSelector((x) => x.authReducer);

  React.useEffect(() => {
    if (!isAuth) {
      navigate('/sign-in');
    }
  }, [isAuth]);

  if (isLoading) {
    return <Loader/>;
  }

  if (currentSession) {
    return (
      <>
        {props.children}
      </>
    );
  }

  return <>Error</>;
};

export default RequireAuth;
