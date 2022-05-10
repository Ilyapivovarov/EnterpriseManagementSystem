import {createAsyncThunk} from "@reduxjs/toolkit";
import {Session, SignIn} from "../../types/authTypes";

const baseUrl = process.env.REACT_APP_API_KEY;

export const resetAuthState = createAsyncThunk(
    'authSlice/reset-auth-state',
    async (_, thunkAPI) => {
        const session = JSON.parse(localStorage.getItem("session")!) as Session | null;
        if (session){
            
            return thunkAPI.fulfillWithValue(session)
        }
        else {
            return thunkAPI.rejectWithValue("error")
        }
    }
)

export const signIn = createAsyncThunk(
    'authSlice/sing-in',
    async (authModel: SignIn, thunkAPI) => {
        const response = await fetch(`${baseUrl}/auth/sign-in`, {
            method: 'POST',
            headers: {
                'content-type': 'application/json;charset=UTF-8',
            },
            body: JSON.stringify(authModel),
        })

        if (response.status == 200) {
            if (response.body) {
                const json = await response.json();
                localStorage.setItem("session", JSON.stringify(json))
                return thunkAPI.fulfillWithValue(json as Session)
            }
        } else {
            console.log(response)
            return thunkAPI.rejectWithValue(response.body)
        }
    }
)

export const signOut = createAsyncThunk(
    'authSlice/sign-out',
    async (thunkAPI) => {
        console.log("sign out")
        localStorage.clear()
    }
)