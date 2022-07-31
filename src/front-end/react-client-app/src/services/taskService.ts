import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { TaskDto } from '../types/taskTypes'
import { RootState } from '../store'
import { resetAuthState } from '../store/AuthReducer/AuthActionCreators'

const baseUrl = process.env.REACT_APP_API_KEY

export const taskApi = createApi({
  reducerPath: 'taskApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${baseUrl}/task`,
    prepareHeaders: (headers, { getState }) => {
      const accessToken = (getState() as RootState).authReducer.currentSession?.accessToken
      if (accessToken) {
        headers.set('authorization', `Bearer ${accessToken}`)
      }
      return headers
    }
  }),
  endpoints: (build) => ({
    getTaskById: build.query<TaskDto, string>({
      query: (id) => ({
        url: `${id}`,
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

export const { useGetTaskByIdQuery } = taskApi
