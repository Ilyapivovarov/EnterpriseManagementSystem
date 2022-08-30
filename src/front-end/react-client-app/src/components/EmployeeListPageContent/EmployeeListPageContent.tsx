import React from 'react';
import {EmployeeDataResponse} from '../../types/accountTypes';
import {Container} from '@mui/material';

interface EmployeeListPageContentProps {
data: EmployeeDataResponse[]
}

const EmployeeListPageContent: React.FC<EmployeeListPageContentProps> = ({data}) => {
  return (
    <Container>

    </Container>
  );
};

export default EmployeeListPageContent;
