import React, {useEffect} from 'react';
import {Alert, Snackbar} from '@mui/material';
import {useAppDispatch, useAppSelector} from '../../hooks';
import {closeNotification} from '../../store/NotificationReduser/notificationReduser';

const Notification: React.FC = () => {
  const {message, show, type} = useAppSelector((x) => x.notificationReducer);
  useEffect(() => {
    if (message) {
      setTimeout(() => onClose(), 3000);
    }
  }, [message]);

  const dispatch = useAppDispatch();

  const onClose = () => dispatch(closeNotification());

  return (
    <Snackbar
      open={show}
      autoHideDuration={50}
      onClick={onClose}
      anchorOrigin={{vertical: 'bottom', horizontal: 'right'}}
    >
      <Alert severity={type}>
        {message}
      </Alert>
    </Snackbar>
  );
};

export default Notification;
