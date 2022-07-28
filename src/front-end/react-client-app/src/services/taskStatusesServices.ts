import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { TaskStatusDto } from '../types/taskTypes'
import { Session } from '../types/authTypes'

// eslint-disable-next-line no-undef
const baseUrl = process.env.REACT_APP_API_KEY

export const taskStatusApi = createApi({
  reducerPath: 'taskStatusApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${baseUrl}/task-status`,
    prepareHeaders: (headers) => {
      const session = JSON.parse(localStorage.getItem('session')!) as Session
      if (session) {
        headers.set('authorization', `Bearer ${session.accessToken}`)
      }
      return headers
    }
  }),
  endpoints: (build) => ({
    getTaskStatuses: build.query<TaskStatusDto[], void>({
      query: () => ({
        url: ``,
      }),
    })
  }),
})

export const { useGetTaskStatusesQuery } = taskStatusApi
