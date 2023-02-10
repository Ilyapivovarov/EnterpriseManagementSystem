import React from 'react';
import {Route, Routes} from 'react-router-dom';
import SignInPage from './pages/SignInPage';
import HomePage from './pages/HomePage';
import SignUpPage from './pages/SignUpPage';
import RequireAuth from './hoc/RequireAuth';
import TaskPage from './pages/TaskPage';
import SettingsPage from './pages/SettingsPage';
import RequireAnonymous from './hoc/RequireAnonymous';
import Layout from './components/Layout/Layout';
import TaskListPage from './pages/TaskListPage/';
import Notification from './components/Notification/Notification';
import CreateTaskPage from './pages/CreateTaskPage';
import EmployeePage from './pages/EmployeePage';
import EmployeeListPage from './pages/EmployeeListPage';

const App: React.FC = () => {
  return (
    <>
      <Notification/>
      <Routes>
        <Route path={'/'} element={<RequireAuth><Layout/></RequireAuth>}>
          <Route index element={<HomePage/>}/>
          <Route path={'employees/'} element={<EmployeeListPage/>}/>
          <Route path={'employees/:id'} element={<EmployeePage/>}/>
          <Route path={'tasks/'} element={<TaskListPage/>}/>
          <Route path={'tasks/:id'} element={<TaskPage/>}/>
          <Route path={'tasks/new'} element={<CreateTaskPage/>}/>
          <Route path={'settings'} element={<SettingsPage/>}/>
        </Route>
        <Route path={'/'} element={<RequireAnonymous/>}>
          <Route path={'sign-in'} element={<SignInPage/>}/>
          <Route path={'sign-up'} element={<SignUpPage/>}/>
        </Route>
        <Route path="*" element={<NotFoundPage/>}/>
      </Routes>
    </>
  );
};

export default App;

const NotFoundPage: React.FC = () => (
  <h1>Not found</h1>
);
