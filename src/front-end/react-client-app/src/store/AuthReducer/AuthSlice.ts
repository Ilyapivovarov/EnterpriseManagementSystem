import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {AuthState} from "./AuthTypes";
import {resetAuthState, signIn} from "./AuthActionCreators";
import {Session} from "../../types/authTypes";

const initialState: AuthState = {
    currentSession: null,
    isAuth: false,
    error: null,
}

export const authSlice = createSlice({
    name: "authSlice",
    initialState,
    reducers: {
        authSuccess(state, action: PayloadAction<Session>) {
            state.isAuth = true;
            state.currentSession = action.payload
        },
        authError(state, action: PayloadAction<string>) {
            state.isAuth = false;
            state.error = action.payload;
        }
    },
    extraReducers: {
        [resetAuthState.fulfilled.type](state, action: PayloadAction<Session>) {
            state.error = null;
            state.isAuth = true;
            state.currentSession = action.payload
        },
        [resetAuthState.rejected.type](state, action: PayloadAction<string>) {
            state.error = action.payload;
            state.isAuth = true;
            state.currentSession = null
        },
        [signIn.fulfilled.type](state, action: PayloadAction<Session>) {
            state.isAuth = true;
            state.currentSession = action.payload
            state.error = null;
        },
        [signIn.rejected.type](state, action: PayloadAction<string>) {
            state.currentSession = null;
            state.isAuth = false;
            state.error = action.payload;
        }
    }
})

export default authSlice.reducer;