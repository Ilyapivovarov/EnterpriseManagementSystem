import {createSlice} from '@reduxjs/toolkit';
import {AuthState} from './AuthTypes';
import {resetAuthState, signIn, signOut, signUp} from './AuthActionCreators';

const initialState: AuthState = {
  currentSession: null,
  error: null,
  isLoading: false,
};

export const authSlice = createSlice({
  name: 'authSlice',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
        .addCase(signIn.pending, (state) => {
          state.isLoading = true;
          state.error = null;
          state.currentSession = null;
        })
        .addCase(signIn.fulfilled, (state, action) => {
          state.isLoading = false;
          state.error = null;
          state.currentSession = action.payload;
        })
        .addCase(signIn.rejected, (state, action) => {
          state.isLoading = false;
          state.error = action.payload;
          state.currentSession = null;
        })
        .addCase(signUp.pending, (state) => {
          state.isLoading = true;
          state.error = null;
          state.currentSession = null;
        })
        .addCase(signUp.fulfilled, (state, action) => {
          state.isLoading = false;
          state.error = null;
          state.currentSession = action.payload;
        })
        .addCase(signUp.rejected, (state, action) => {
          state.isLoading = false;
          state.error = action.payload;
          state.currentSession = null;
          console.log(action.payload);
        })
        .addCase(resetAuthState.fulfilled, (state, action) => {
          state.isLoading = false;
          state.currentSession = action.payload;
          state.error = null;
        })
        .addCase(resetAuthState.rejected, () => {
          return initialState;
        })
        .addCase(signOut.fulfilled, () => {
          return initialState;
        });
  },
});

export default authSlice.reducer;
