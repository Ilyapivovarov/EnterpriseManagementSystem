import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {EmployeeDataResponse} from '../types/accountTypes';
import {resetAuthState} from '../store/AuthReducer/AuthActionCreators';
import {RootState, store} from '../store';
import {BaseUrl} from '../helpers/Constants';

export const employeeApi = createApi({
  reducerPath: 'employeeApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${BaseUrl}/`,
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
        url: `/employee/${guid}`,
      }),
    }),
    getEmployeeById: build.query<EmployeeDataResponse, string>({
      query: (id) => ({
        url: `/employee/${id}`,
      }),
    }),
    getEmployeesByPage: build.query<EmployeeDataResponse[], {pageNumber: number, pageSize: number}>({
      query: ({pageNumber, pageSize}) => ({
        url: '/employee',
        params: {
          pageNumber, pageSize,
        },
      }),
    }),
  }),
});

export const {useGetEmployeeByGuidQuery, useGetEmployeeByIdQuery, useGetEmployeesByPageQuery} = employeeApi;
