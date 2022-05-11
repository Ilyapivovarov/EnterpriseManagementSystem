import {createAsyncThunk} from "@reduxjs/toolkit";
import {Session, SignIn} from "../../types/authTypes";

const baseUrl = process.env.REACT_APP_API_KEY;

export const resetAuthState = createAsyncThunk<Session, void, { rejectValue: string }>(
    'authSlice/reset-auth-state',
    async (_, {rejectWithValue}) => {
        const session = JSON.parse(localStorage.getItem("session")!) as Session | null;
        if (session) {
            return session
        }

        return rejectWithValue("error")
    }
)

export const signIn = createAsyncThunk<Session, SignIn, { rejectValue: string }>(
    'authSlice/sing-in',
    async (authModel, {rejectWithValue}) => {
        const response = await fetch(`${baseUrl}/auth/sign-in`, {
            method: 'POST',
            headers: {
                'content-type': 'application/json;charset=UTF-8',
            },
            body: JSON.stringify(authModel),
        })

        if (response.ok) {
            return await response.json();
        }

        console.log(response)
        return rejectWithValue("response")
    }
)

export const signOut = createAsyncThunk<void, void>(
    'authSlice/sign-out',
    async () => {
        console.log("sign out")
        localStorage.clear()
    }
)