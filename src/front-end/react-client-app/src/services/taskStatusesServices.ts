import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { TaskStatusDto } from '../types/taskTypes'
import { RootState } from '../store'
import { resetAuthState } from '../store/AuthReducer/AuthActionCreators'

// eslint-disable-next-line no-undef
const baseUrl = process.env.REACT_APP_API_KEY

export const taskStatusApi = createApi({
  reducerPath: 'taskStatusApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${baseUrl}/task-status`,
    prepareHeaders: (headers, { getState }) => {
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
      }),
      onQueryStarted: async (_, { dispatch }) => {
        try {
          dispatch(resetAuthState())
        } catch (e) {
          console.error('Error while reset auth state', e)
        }
      },
    })
  }),
})

export const { useGetTaskStatusesQuery } = taskStatusApi
