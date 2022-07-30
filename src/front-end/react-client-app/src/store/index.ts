import { combineReducers, configureStore } from '@reduxjs/toolkit'
import { authApi } from '../services/authService'
import { accountApi } from '../services/accountService'
import authReducer from './AuthReducer/AuthSlice'
import { taskApi } from '../services/taskService'
import { taskStatusApi } from '../services/taskStatusesServices'
import { executorService } from '../services/executorService'

const rootReducers = combineReducers({
  authReducer,
  [authApi.reducerPath]: authApi.reducer,
  [taskApi.reducerPath]: taskApi.reducer,
  [accountApi.reducerPath]: accountApi.reducer,
  [taskStatusApi.reducerPath]: taskStatusApi.reducer,
  [executorService.reducerPath]: executorService.reducer,
})

export const store = configureStore({
  reducer: rootReducers,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(authApi.middleware)
      .concat(accountApi.middleware)
      .concat(taskApi.middleware)
      .concat(taskStatusApi.middleware)
      .concat(executorService.middleware)
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
