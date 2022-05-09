import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {authApi} from "../services/authService";
import {accountApi} from "../services/accountService";


const rootReducers = combineReducers({
    [authApi.reducerPath]: authApi.reducer,
    [accountApi.reducerPath] : accountApi.reducer
})

export const store = configureStore({
        reducer: rootReducers,
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware()
                .concat(authApi.middleware)
                .concat(accountApi.middleware)
    })

// export type RootState = ReturnType<typeof store.getState>
// export type AppDispatch = typeof store.dispatch