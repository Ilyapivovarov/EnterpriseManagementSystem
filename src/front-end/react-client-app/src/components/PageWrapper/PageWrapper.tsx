import React from 'react';
import {Paper} from '@mui/material';

const PageWrapper: React.FC = ({children}) => {
  return (
    <Paper
      elevation={3}
      sx={{
        p: 2,
        marginTop: '10px',
        display: 'flex',
        flexDirection: 'column',
        height: '100%',
        width: '100%',
        minHeight: '75vh',
      }}
    >
      {children}
    </Paper>

  );
};

export default PageWrapper;
