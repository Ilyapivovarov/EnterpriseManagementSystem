import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { TaskStatusDto } from '../types/taskTypes'
import { RootState, store } from '../store'
import { resetAuthState } from '../store/AuthReducer/AuthActionCreators'

const baseUrl = process.env.REACT_APP_API_KEY

export const taskStatusApi = createApi({
  reducerPath: 'taskStatusApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${baseUrl}/task-status`,
    prepareHeaders: (headers, { getState }) => {
      store.dispatch(resetAuthState())
      const accessToken = (getState() as RootState).authReducer.currentSession?.accessToken
      if (accessToken) {
        headers.set('authorization', `Bearer ${accessToken}`)
      }
      return headers
    }
  }),
  endpoints: (build) => ({
    getTaskStatuses: build.query<TaskStatusDto[], void>({
      query: () => ({
        url: ``,
      })
    })
  }),
})

export const { useGetTaskStatusesQuery } = taskStatusApi
