import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {Session, SignIn} from '../types/authTypes';

export const authApi = createApi({
    reducerPath: 'authApi',
    baseQuery: fetchBaseQuery({
        baseUrl: 'https://localhost:7104/auth',
    }),
    endpoints: (build) => ({
        signIn: build.mutation<Session, SignIn>({
            query: (session) => ({
                url: "sign-in",
                method: "POST",
                body: session
            }),
        }),
    }),
})

export const {useSignInMutation} = authApi;
