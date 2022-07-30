import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/dist/query/react'
import { UsersByPageDto } from '../types/taskTypes'
import { Session } from '../types/authTypes'

const baseUrl = process.env.REACT_APP_API_KEY

export const executorService = createApi({
  reducerPath: 'executorService',
  baseQuery: fetchBaseQuery({
    baseUrl: `${baseUrl}`,
    prepareHeaders: (headers) => {
      const session = JSON.parse(localStorage.getItem('session')!) as Session
      if (session) {
        headers.set('authorization', `Bearer ${session.accessToken}`)
      }
      return headers
    },
  }),
  endpoints: (build) => ({
    getUsersByPage: build.query<UsersByPageDto, { page: number, count: number }>({
      query: ({
        page,
        count
      }) => ({
        url: `user?page=${page}&count=${count}`,
        method: 'GET',

      }),
    })
  }),
})

export const { useGetUsersByPageQuery } = executorService
