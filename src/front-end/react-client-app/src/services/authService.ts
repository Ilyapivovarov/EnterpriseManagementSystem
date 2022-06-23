import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {Session, SignIn, SignUp} from '../types/authTypes';

const baseUrl = process.env.REACT_APP_API_KEY;

export const authApi = createApi({
    reducerPath: 'authApi',
    baseQuery: fetchBaseQuery({
        baseUrl: `${baseUrl}/auth`,
    }),
    endpoints: (build) => ({
        signIn: build.mutation<Session, SignIn>({
            query: (session) => ({
                url: "sign-in",
                method: "POST",
                body: session
            }),
        }),
        signUp: build.mutation<Session, SignUp>({
            query: (session) => ({
                url: "sign-up",
                method: "POST",
                body: session
            }),
        }),
    }),
})

export const {useSignInMutation, useSignUpMutation} = authApi;
