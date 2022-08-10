import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/dist/query/react';
import {UsersByPageDto} from '../types/taskTypes';
import {RootState, store} from '../store';
import {resetAuthState} from '../store/AuthReducer/AuthActionCreators';

const baseUrl = process.env.REACT_APP_API_KEY;

export const executorService = createApi({
  reducerPath: 'executorService',
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
    getExecutorsByPage: build.query<UsersByPageDto, { page: number, count: number }>({
      query: ({
        page,
        count,
      }) => ({
        url: `user?page=${page}&count=${count}`,
        method: 'GET',

      }),
    }),
  }),
});

export const {useGetExecutorsByPageQuery, useLazyGetExecutorsByPageQuery} = executorService;
