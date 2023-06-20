import React from 'react';
import {EmployeeDataResponse} from '../../types/accountTypes';

interface EmployeePageContentProps {
  employee: EmployeeDataResponse
}

const EmployeePageContent: React.FC<EmployeePageContentProps> = ({employee}) => {
  return (
    <div>
      {employee.user.firstName}
    </div>
  );
};

export default EmployeePageContent;
