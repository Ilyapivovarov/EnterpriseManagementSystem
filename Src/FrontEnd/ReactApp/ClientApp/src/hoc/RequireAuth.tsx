import React from 'react';
import {useNavigate} from 'react-router-dom';
import {useAppSelector} from '../hooks';
import Loader from '../components/Loader/Loader';

const RequireAuth: React.FC = (props) => {
  const navigate = useNavigate();
  const {currentSession, isLoading, isAuth, error} = useAppSelector((x) => x.authReducer);

  React.useEffect(() => {
    if (!isAuth && !isLoading) {
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

  return <>{error} asfasf</>;
};

export default RequireAuth;
