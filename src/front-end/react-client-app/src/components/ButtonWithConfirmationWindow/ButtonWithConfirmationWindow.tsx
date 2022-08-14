import React from 'react';
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText, DialogTitle,
} from '@mui/material';

interface ButtonWithConfirmationWindowProps {
  title?: string,
  content?: string,
  onClickAgree?: () => void
  onClickDisagree?: () => void
}

const ButtonWithConfirmationWindow: React.FC<ButtonWithConfirmationWindowProps> = ({
  title,
  content,
  onClickAgree,
  onClickDisagree,
  children}) => {
  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const onClickAgreeHandler = () =>{
    if (onClickAgree) {
      onClickAgree();
    }
    setOpen(false);
  };

  const onDisagreeHandler = () =>{
    if (onClickDisagree) {
      onClickDisagree();
    }
    setOpen(false);
  };

  return (
    <div>
      <Button onClick={handleClickOpen}>
        {children}
      </Button>
      <Dialog
        open={open}
        keepMounted
        aria-describedby="alert-dialog-slide-description"
      >
        <DialogContent>
          <DialogTitle>{title}</DialogTitle>
          <DialogContentText id="alert-dialog-slide-description">
            {content}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={onDisagreeHandler}>Disagree</Button>
          <Button onClick={onClickAgreeHandler}>Agree</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};

export default ButtonWithConfirmationWindow;
