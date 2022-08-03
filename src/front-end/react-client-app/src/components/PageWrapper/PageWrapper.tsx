import React, {FC} from 'react';
import {Paper} from '@mui/material';

const PageWrapper: FC = ({children}) => {
  return (
    <Paper
      sx={{
        p: 2,
        display: 'flex',
        flexDirection: 'column',
        height: '100%',
        width: '100%',
        minHeight: '80vh',
      }}
    >
      {children}
    </Paper>

  );
};

export default PageWrapper;
