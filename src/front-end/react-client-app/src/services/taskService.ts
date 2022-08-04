import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {TaskDto} from '../types/taskTypes';
import {RootState, store} from '../store';
import {resetAuthState} from '../store/AuthReducer/AuthActionCreators';

const baseUrl = process.env.REACT_APP_API_KEY;

export const taskApi = createApi({
  reducerPath: 'taskApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${baseUrl}`,
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
    getTaskById: build.query<TaskDto, string>({
      query: (id) => ({
        url: `/task/${id}`,
        validateStatus: () => true,
      }),
    }),
    getTasksByPage: build.query<TaskDto[], { pageNumber: number, pageSize: number }>({
      query: ({pageNumber, pageSize}) => ({
        url: `/task?pageNumber=${pageNumber}&pageSize=${pageSize}`,
        validateStatus: () => true,
      }),
    }),
  }),
});

export const {useGetTaskByIdQuery, useGetTasksByPageQuery} = taskApi;
