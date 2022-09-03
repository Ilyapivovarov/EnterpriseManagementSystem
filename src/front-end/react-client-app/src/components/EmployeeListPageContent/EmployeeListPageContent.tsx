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
  const [filteredEmployees, setFilteredEmployees] = React.useState(data);

  const onChangeFilterHandler = (e : React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    const value = e.target.value;
    if (value.length > 3) {
      setFilteredEmployees(data.filter((x) =>
        (x.user.firstName.includes(value.trim()) || x.user.lastName.includes(value.trim()) ||
         x.user.emailAddress.includes(value))));
    }
    if (value.length == 0) {
      setFilteredEmployees(data);
    }
  };

  return (
    <Box>
      <Box p={'10px'}>
        <TextField
          onChange={onChangeFilterHandler}
          label={'Search employee'}
          size={'small'}
        />
      </Box>
      <Box
        p={'5px'}
        m={'10px'}
        border={'solid 1px #eeeeee'}
        minHeight={'75vh'}
        borderRadius={'5px'}
      >
        {filteredEmployees.length == 0 &&
          <Box textAlign={'center'} paddingTop={'90px'}>
            <Typography fontSize={60} color={'gray'}>Not found employee</Typography>
          </Box>}
        {filteredEmployees.length > 0 && filteredEmployees.map((x) =>
          <Card key={x.id}
            style={{display: 'flex', justifyContent: 'space-between'}}
          >
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
                onClick={() => navigate(`/employees/${x.user.identityGuid}`)}
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
