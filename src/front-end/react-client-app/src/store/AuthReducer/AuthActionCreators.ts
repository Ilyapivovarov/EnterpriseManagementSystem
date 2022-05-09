import {createAsyncThunk} from "@reduxjs/toolkit";
import {Session, SignIn} from "../../types/authTypes";

export const resetAuthState = createAsyncThunk(
    'authSlice/reset-auth-state',
    async (_, thunkAPI) => {
        const session = localStorage.getItem("session") as Session | null;
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
        const response = await fetch('https://localhost:7104/auth/sign-in', {
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
                return thunkAPI.fulfillWithValue(json)
            }
        } else {
            console.log(response)
            return thunkAPI.rejectWithValue(response.body)
        }
    }
)

// export const signUp = createAsyncThunk(
//     'authSlice/sign-up',
//     async (authModel: SignUp, thunkAPI) => {
//         const response = await Axios.post<JwtTokenResponse>("auth/sign-up", authModel)
//         if (response.status == 200) {
//             const account = jwtDecode<JwtTokenDecode>(response.data.access_token)
//             if (account) {
//                 localStorage.setItem(AccessTokenKey, response.data.access_token)
//                 return account;
//             }
//         } else {
//             return thunkAPI.rejectWithValue(response.data)
//         }
//     }
// )
//
// export const validateToken = createAsyncThunk(
//     'authSlice/validateToken',
//     async (_, thunkAPI) => {
//         try {
//             const token = localStorage.getItem("session");
//             if (token) {
//                 const account = jwtDecode<JwtTokenDecode>(token);
//                 if (account && IsTokenExpValid(account.exp)) {
//                     return account;
//                 }
//             }
//             return thunkAPI.rejectWithValue("Update auth");
//         } catch {
//             return thunkAPI.rejectWithValue("Unknown error while validate token");
//         }
//     });
//
// const IsTokenExpValid = (tokenExp: number): boolean => {
//     const exp = new Date(0);
//     exp.setUTCSeconds(tokenExp);
//     let now = new Date();
//     return exp > now
// }
//
// export const signOut = createAsyncThunk(
//     "authSlice/signOut",
//     async (_, thunkAPI) => {
//         localStorage.clear();
//     })