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

        localStorage.clear();
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
            console.log("ok")
            return await response.json();
        }

        return rejectWithValue(await response.text())
    }
)

export const signOut = createAsyncThunk<void, void>(
    'authSlice/sign-out',
    async () => {
        console.log("sign out")
        localStorage.clear()
    }
)