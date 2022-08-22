import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {EmployeeDataResponse} from '../types/accountTypes';
import {resetAuthState} from '../store/AuthReducer/AuthActionCreators';
import {RootState, store} from '../store';

const baseUrl = process.env.REACT_APP_API_KEY;

export const employeeApi = createApi({
  reducerPath: 'employeeApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${baseUrl}/employee`,
    prepareHeaders: (headers, {getState}) => {
      store.dispatch(resetAuthState());
      const accessToken = (getState() as RootState).authReducer.currentSession?.accessToken;
      if (accessToken) {
        headers.set('authorization', `Bearer ${accessToken}`);
      }
      return headers;
    },
  }),

  endpoints: (build) => ({
    getEmployeeByGuid: build.query<EmployeeDataResponse, string>({
      query: (guid) => ({
        url: `/${guid}`,
      }),
    }),
  }),
});

export const {useGetEmployeeByGuidQuery} = employeeApi;
