import React from 'react';
import {Typography} from '@mui/material';

interface ErrorViewProps {
    errorMessage: string
}

const ErrorView: React.FC<ErrorViewProps> = ({errorMessage}) => {
  return (
    <Typography>
      {errorMessage}
    </Typography>
  );
};

export default ErrorView;
