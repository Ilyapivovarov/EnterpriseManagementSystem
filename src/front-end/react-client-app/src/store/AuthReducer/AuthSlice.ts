import { createSlice } from '@reduxjs/toolkit'
import { AuthState } from './AuthTypes'
import { resetAuthState, signIn, signOut, signUp } from './AuthActionCreators'

const initialState: AuthState = {
  currentSession: null,
  error: null,
  isLoading: false
}

export const authSlice = createSlice({
  name: 'authSlice',
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(signIn.pending, (state) => {
        state.error = null
        state.isLoading = true
      })
      .addCase(signIn.fulfilled, (state, action) => {
        state.isLoading = false
        state.currentSession = action.payload

        localStorage.setItem('session', JSON.stringify(state.currentSession))
      })
      .addCase(signIn.rejected, (state, action) => {
        state.isLoading = false
        state.currentSession = null
        state.error = action.payload
      })
      .addCase(signUp.pending, (state) => {
        state.error = null
        state.isLoading = true
      })
      .addCase(signUp.fulfilled, (state, action) => {
        state.isLoading = false
        state.currentSession = action.payload

        localStorage.setItem('session', JSON.stringify(state.currentSession))
      })
      .addCase(signUp.rejected, (state, action) => {
        state.isLoading = false
        state.currentSession = null
        state.error = action.payload
      })
      .addCase(resetAuthState.pending, (state) => {
        state.isLoading = true
        state.error = null
      })
      .addCase(resetAuthState.fulfilled, (state, action) => {
        state.isLoading = false
        state.currentSession = action.payload
        state.error = null
        localStorage.setItem('session', JSON.stringify(state.currentSession))
      })
      .addCase(resetAuthState.rejected, () => {
        return initialState
      })
      .addCase(signOut.fulfilled, () => {
        return initialState
      })

  }
})

export default authSlice.reducer
