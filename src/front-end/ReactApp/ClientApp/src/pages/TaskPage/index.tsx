import React from 'react';
import {useGetTaskByIdQuery} from '../../api/taskApi';
import {useParams} from 'react-router-dom';
import Loader from '../../components/Loader/Loader';
import TaskPageContent from '../../components/TaskPageContent/TaskPageContent';
import {Breadcrumbs, Paper} from '@mui/material';
import Link from '../../components/Link/Link';
import {useTranslation} from 'react-i18next';
import {TranslationKeys} from '../../i18n';

const TaskPage: React.FC = () => {
  const {t} = useTranslation();
  const {id} = useParams();
  const taskId = Number.parseInt(id!);
  const {data, isLoading, isSuccess, error} = useGetTaskByIdQuery(taskId);

  if (isLoading) {
    return <Loader/>;
  }
  if (isSuccess) {
    return <>
      <Paper sx={{p: 2, marginTop: '10px'}} elevation={1}>
        <Breadcrumbs aria-label="breadcrumb">
          <Link to={'/'}>
            {t(TranslationKeys.pages.home)}
          </Link>
          <Link to={'/tasks'}>
            {t(TranslationKeys.pages.tasks)}
          </Link>
          <Link to={'#'}>
           #{data.id}
          </Link>
        </Breadcrumbs>
      </Paper>
      <TaskPageContent taskDto={data}/>
    </>;
  }

  return <>{JSON.parse(JSON.stringify(error)).data}</>;
};

export default TaskPage;
