import React from 'react';
import {EmployeeDataResponse} from '../../types/accountTypes';
import {Box, Button, Card, CardActions, CardContent, TextField, Typography} from '@mui/material';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import {useNavigate} from 'react-router-dom';

interface EmployeeListPageContentProps {
  data: EmployeeDataResponse[]
}

const EmployeeListPageContent: React.FC<EmployeeListPageContentProps> = ({data}) => {
  const navigate = useNavigate();

  return (
    <Box>
      <Box p={'5px'}>
        <TextField
          label={'Fullname filter'}
          size={'small'}
        />
      </Box>
      <Box p={'5px'}>
        {data.map((x) =>
          <Card key={x.id}
            style={{display: 'flex', justifyContent: 'space-between'}}>
            <CardContent>
              <Typography variant="h5" component="div">
                {x.user.firstName} {x.user.lastName}
              </Typography>
              <Typography sx={{mb: 1.5}} color="text.secondary">
                {x.user.emailAddress}
              </Typography>
              <Typography sx={{mb: 1.5}} color="text.secondary">
                {x.position?.name}
              </Typography>
            </CardContent>
            <CardActions>
              <Button
                size="medium"
                endIcon={<ArrowForwardIcon/>}
                onClick={() => navigate(`/employees/${x.id}`)}
              >
                Go to profile
              </Button>
            </CardActions>
          </Card>,
        )}
      </Box>
    </Box>
  );
};

export default EmployeeListPageContent;
