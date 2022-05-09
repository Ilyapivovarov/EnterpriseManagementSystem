import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {Session, SignIn} from '../types/authTypes';
import {BaseQueryArg} from "@reduxjs/toolkit/dist/query/baseQueryTypes";
import {Account} from "../types/accountTypes";

const baseUrl = process.env.REACT_APP_API_KEY;

export const accountApi = createApi({
    reducerPath: "accountApi",
    baseQuery: fetchBaseQuery({
        baseUrl: `${baseUrl}/user`,
        prepareHeaders: (headers) => {
            const session = JSON.parse(localStorage.getItem("session")!) as Session;
            if (session)
                headers.set('authorization', `Bearer ${session.accessToken}`)
            return headers;
        },
    }),
    endpoints: (build) => ({
        getAccountByGuid: build.query<Account, string>({
            query: (guid) =>({
              url: `/${guid}`,
            })
        })
    }),
})

export const {useGetAccountByGuidQuery} = accountApi;
