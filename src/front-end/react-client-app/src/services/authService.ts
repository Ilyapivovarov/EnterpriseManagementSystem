import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';

const authApi = createApi({
    reducerPath: 'authApi',
    baseQuery: fetchBaseQuery({
        baseUrl: '/',
    }),
    endpoints: (build) => ({
        signIn: build.mutation<string, Session>({
            query: (session) => ({
                url: "sign-in",
                method: "PUT",
                body: session
            }),
        }),
    }),
})

export const {useSignInMutation} = authApi;
