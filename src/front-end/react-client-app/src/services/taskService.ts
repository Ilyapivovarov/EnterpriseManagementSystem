import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {TaskDto} from "../types/taskTypes";
import {Session} from "../types/authTypes";

const baseUrl = process.env.REACT_APP_API_KEY;

export const taskApi = createApi({
    reducerPath: 'taskApi',
    baseQuery: fetchBaseQuery({
        baseUrl: `${baseUrl}/task`,
        prepareHeaders: (headers) => {
            const session = JSON.parse(localStorage.getItem("session")!) as Session;
            if (session)
                headers.set('authorization', `Bearer ${session.accessToken}`)
            return headers;
        }
    }),
    endpoints: (build) => ({
        getTaskById: build.query<TaskDto, string>({
            query: (id) => ({
                url: `${id}`,
            }),
        })
    }),
})

export const {useGetTaskByIdQuery} = taskApi;
