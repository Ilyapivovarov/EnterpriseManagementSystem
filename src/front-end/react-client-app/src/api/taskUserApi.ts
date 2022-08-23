import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/dist/query/react';
import {UsersByPageDto} from '../types/taskTypes';
import {RootState, store} from '../store';
import {resetAuthState} from '../store/AuthReducer/AuthActionCreators';
import {BaseUrl} from '../helpers/Constants';

export const taskUserApi = createApi({
  reducerPath: 'taskUserApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${BaseUrl}`,
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
    getUsersByPage: build.query<UsersByPageDto, { page: number, count: number }>({
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

export const {useGetUsersByPageQuery, useLazyGetUsersByPageQuery} = taskUserApi;
