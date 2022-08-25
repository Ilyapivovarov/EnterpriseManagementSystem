import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {CreateTaskDto, TaskDto} from '../types/taskTypes';
import {RootState, store} from '../store';
import {resetAuthState} from '../store/AuthReducer/AuthActionCreators';
import {BaseUrl} from '../helpers/Constants';

export const taskApi = createApi({
  reducerPath: 'taskApi',
  tagTypes: ['task'],
  baseQuery: fetchBaseQuery({
    baseUrl: `${BaseUrl}`,
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
      query: ({
        pageNumber,
        pageSize,
      }) => ({
        url: `/task?pageNumber=${pageNumber}&pageSize=${pageSize}`,
      }),
      providesTags: ['task'],
    }),
    updateTaskStatus: build.mutation<void, { taskId: number, statusId: number }>({
      query: ({
        taskId,
        statusId,
      }) => ({
        url: `/task/status`,
        method: 'PUT',
        body: {
          taskId,
          statusId,
        },
      }),
      invalidatesTags: ['task'],
    }),
    updateTaskExecutor: build.mutation<void, { taskId: number, executorId?: number }>({
      query: ({
        taskId,
        executorId,
      }) => ({
        url: `/task/executor`,
        method: 'PUT',
        body: {
          taskId,
          executorId,
        },
      }),
      invalidatesTags: ['task'],
    }),
    setInspector: build.mutation<void, { taskId: number, inspectorId?: number }>({
      query: ({
        taskId,
        inspectorId,
      }) => ({
        url: `/task/inspector`,
        method: 'PUT',
        body: {
          taskId,
          inspectorId,
        },
      }),
      invalidatesTags: ['task'],
    }),
    updateTask: build.mutation<void, { id: number, guid: string, name: string, description?: string }>({
      query: (arg) => ({
        url: `/task/`,
        method: 'PUT',
        body: {
          ...arg,
        },
      }),
      invalidatesTags: ['task'],
    }),
    createTask: build.mutation<void, CreateTaskDto>({
      query: (arg) => ({
        url: `/task/`,
        method: 'POST',
        body: {
          ...arg,
        },
      }),
      invalidatesTags: ['task'],
    }),
    removeTask: build.mutation<void, number>({
      query: (arg) => ({
        url: `/task/${arg}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['task'],
    }),
  }),
});

export const {
  useGetTaskByIdQuery,
  useGetTasksByPageQuery,
  useUpdateTaskStatusMutation,
  useUpdateTaskExecutorMutation,
  useUpdateTaskMutation,
  useSetInspectorMutation,
  useCreateTaskMutation,
  useRemoveTaskMutation,
} = taskApi;
