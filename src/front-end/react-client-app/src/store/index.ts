import {combineReducers, configureStore} from '@reduxjs/toolkit';
import {employeeApi} from '../services/employeeService';
import authReducer from './AuthReducer/AuthSlice';
import {taskApi} from '../services/taskService';
import {taskStatusApi} from '../services/taskStatusesServices';
import {executorService} from '../services/executorService';

const rootReducers = combineReducers({
  authReducer,
  [taskApi.reducerPath]: taskApi.reducer,
  [employeeApi.reducerPath]: employeeApi.reducer,
  [taskStatusApi.reducerPath]: taskStatusApi.reducer,
  [executorService.reducerPath]: executorService.reducer,
});

export const store = configureStore({
  reducer: rootReducers,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
        .concat(employeeApi.middleware)
        .concat(taskApi.middleware)
        .concat(taskStatusApi.middleware)
        .concat(executorService.middleware),
});

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
