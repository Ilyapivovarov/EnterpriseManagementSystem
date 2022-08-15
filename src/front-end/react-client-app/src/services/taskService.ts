import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {TaskDto} from '../types/taskTypes';
import {RootState, store} from '../store';
import {resetAuthState} from '../store/AuthReducer/AuthActionCreators';

const baseUrl = process.env.REACT_APP_API_KEY;

export const taskApi = createApi({
  reducerPath: 'taskApi',
  tagTypes: ['task'],
  baseQuery: fetchBaseQuery({
    baseUrl: `${baseUrl}`,
    prepareHeaders: async (headers, {getState}) => {
      await store.dispatch(resetAuthState());
      const accessToken = (getState() as RootState).authReducer.currentSession?.accessToken;
      if (accessToken) {
        headers.set('authorization', `Bearer ${accessToken}`);
      }
      return headers;
    },
  }),
  endpoints: (build) => ({
    getTaskById: build.query<TaskDto, number>({
      query: (id) => ({
        url: `/task/${id}`,
      }),
      providesTags: ['task'],
    }),
    getTasksByPage: build.query<TaskDto[], { pageNumber: number, pageSize: number }>({
      query: ({pageNumber, pageSize}) => ({
        url: `/task?pageNumber=${pageNumber}&pageSize=${pageSize}`,
      }),
      providesTags: ['task'],
    }),
    updateTaskStatus: build.mutation<void, {taskId: number, statusId: number}>({
      query: ({taskId, statusId}) => ({
        url: `/task/status?taskId=${taskId}&statusId=${statusId}`,
        method: 'PUT',
      }),
      invalidatesTags: ['task'],
    }),
    updateTaskExecutor: build.mutation<void, {taskId: number, executorId: number}>({
      query: ({taskId, executorId}) => ({
        url: `/task/executor?taskId=${taskId}&executorId=${executorId}`,
        method: 'PUT',
      }),
      invalidatesTags: ['task'],
    }),
    updateTask: build.mutation<void, {id: number, guid: string, name: string, description?: string}>({
      query: (arg) => ({
        url: `/task/`,
        method: 'PUT',
        body: {
          ...arg,
        },
      }),
      invalidatesTags: ['task'],
    }),
  }),
});

export const {useGetTaskByIdQuery, useGetTasksByPageQuery, useUpdateTaskStatusMutation, useUpdateTaskExecutorMutation, useUpdateTaskMutation} = taskApi;
