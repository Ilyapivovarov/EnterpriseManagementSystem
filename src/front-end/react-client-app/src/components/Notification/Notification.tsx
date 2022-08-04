import React from 'react';
import {Snackbar} from '@mui/material';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';
import {SnackbarCloseReason} from '@mui/material/Snackbar/Snackbar';

interface NotificationProps {
  message: string,
  isOpen: boolean,
  onClose: (event: React.SyntheticEvent<any> | Event, reason: SnackbarCloseReason) => void;
}

const Notification: React.FC<NotificationProps> = ({message, isOpen, onClose}) => {
  return (
    <Snackbar
      open={isOpen}
      autoHideDuration={5000}
      onClose={onClose}
      message={message}
      anchorOrigin={{vertical: 'bottom', horizontal: 'center'}}
      action={
        <React.Fragment>
          <IconButton
            aria-label="close"
            color="inherit"
            sx={{p: 0.5}}
            onClick={(e) => onClose(e, 'timeout')}
          >
            <CloseIcon />
          </IconButton>
        </React.Fragment>
      }
    />
  );
};

export default Notification;
