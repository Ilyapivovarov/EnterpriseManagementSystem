import {combineReducers, configureStore} from '@reduxjs/toolkit';
import {employeeApi} from '../api/employeeApi';
import authReducer from './AuthReducer/AuthSlice';
import {taskApi} from '../api/taskApi';
import {taskStatusApi} from '../api/taskStatusApi';
import {taskUserApi} from '../api/taskUserApi';
import notificationReducer from './NotificationReduser/notificationReduser';


const rootReducers = combineReducers({
  authReducer,
  notificationReducer,
  [taskApi.reducerPath]: taskApi.reducer,
  [employeeApi.reducerPath]: employeeApi.reducer,
  [taskStatusApi.reducerPath]: taskStatusApi.reducer,
  [taskUserApi.reducerPath]: taskUserApi.reducer,
});

export const store = configureStore({
  reducer: rootReducers,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
        .concat(employeeApi.middleware)
        .concat(taskApi.middleware)
        .concat(taskStatusApi.middleware)
        .concat(taskUserApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
