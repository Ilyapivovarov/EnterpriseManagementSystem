import React, {useEffect} from 'react';
import {Snackbar} from '@mui/material';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';
import {useAppDispatch, useAppSelector} from '../../hooks';
import {closeNotification} from '../../store/NotificationReduser/notificationReduser';

const Notification: React.FC = () => {
  const {message, show} = useAppSelector((x) => x.notificationReducer);
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
      message={message}
      anchorOrigin={{vertical: 'bottom', horizontal: 'center'}}
      action={
        <React.Fragment>
          <IconButton
            aria-label="close"
            color="inherit"
            sx={{p: 0.5}}
            // onClick={() => onClose()}
          >
            <CloseIcon />
          </IconButton>
        </React.Fragment>
      }
    />
  );
};

export default Notification;
