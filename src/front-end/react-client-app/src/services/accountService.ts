import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {Session} from '../types/authTypes';
import {EmployeeDataResponse} from "../types/accountTypes";

const baseUrl = process.env.REACT_APP_API_KEY;

export const accountApi = createApi({
    reducerPath: "accountApi",
    baseQuery: fetchBaseQuery({
        baseUrl: `${baseUrl}/employee`,
        prepareHeaders: (headers) => {
            const session = JSON.parse(localStorage.getItem("session")!) as Session;
            if (session)
                headers.set('authorization', `Bearer ${session.accessToken}`)
            return headers;
        },
    }),
    endpoints: (build) => ({
        getAccountByGuid: build.query<EmployeeDataResponse, string>({
            query: (guid) => ({
                url: `/${guid}`,
            })
        })
    }),
})

export const {useGetAccountByGuidQuery} = accountApi;
