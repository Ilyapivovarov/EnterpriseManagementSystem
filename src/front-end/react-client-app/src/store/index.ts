import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {authApi} from "../services/authService";


const rootReducers = combineReducers({
    [authApi.reducerPath]: authApi.reducer
})

export const store = configureStore({
        reducer: rootReducers,
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware()
                .concat(authApi.middleware)
    })



export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch