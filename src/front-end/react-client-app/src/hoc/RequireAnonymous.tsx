import React from 'react';
import {Outlet, useNavigate} from 'react-router-dom';
import {useAppSelector} from '../hooks';
import Loader from '../components/Loader/Loader';

const RequireAnonymous: React.FC = () => {
  const navigate = useNavigate();
  const {isLoading, isAuth} = useAppSelector((x) => x.authReducer);

  React.useEffect(() => {
    if (!isAuth) {
      navigate('/');
    }
  }, [isAuth]);

  if (isLoading) {
    return <Loader/>;
  }

  return (
    <Outlet/>
  );
};

export default RequireAnonymous;
