import React from 'react';
import {EmployeeDataResponse} from '../../types/accountTypes';

interface EmployeeListPageContentProps {
data: EmployeeDataResponse[]
}

const EmployeeListPageContent: React.FC<EmployeeListPageContentProps> = ({data}) => {
  return (
    <div>

    </div>
  );
};

export default EmployeeListPageContent;
