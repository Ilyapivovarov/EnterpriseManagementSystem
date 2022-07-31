import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { EmployeeDataResponse } from '../types/accountTypes'
import { resetAuthState } from '../store/AuthReducer/AuthActionCreators'
import { RootState } from '../store'

const baseUrl = process.env.REACT_APP_API_KEY

export const accountApi = createApi({
  reducerPath: 'accountApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${baseUrl}/employee`,
    prepareHeaders: (headers, { getState }) => {
      const accessToken = (getState() as RootState).authReducer.currentSession?.accessToken
      if (accessToken) {
        headers.set('authorization', `Bearer ${accessToken}`)
      }
      return headers
    }
  }),

  endpoints: (build) => ({
    getAccountByGuid: build.query<EmployeeDataResponse, string>({
      query: (guid) => ({
        url: `/${guid}`,
        validateStatus: () => true
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

export const { useGetAccountByGuidQuery } = accountApi
