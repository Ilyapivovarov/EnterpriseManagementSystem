import type {PayloadAction} from '@reduxjs/toolkit';
import {createSlice} from '@reduxjs/toolkit';

interface CounterState {
  message: string | null,
  show: boolean;
  type: 'success' | 'error',
}

const initialState: CounterState = {
  show: false,
  message: null,
  type: 'success',
};

export const notificationSlice = createSlice({
  name: 'notification',
  initialState,
  reducers: {
    showNotification: (state, action: PayloadAction<{ message: string, type: 'success' | 'error' }>) => {
      state.show = true;
      state.message = action.payload.message;
      state.type = action.payload.type;
    },
    closeNotification: (state) => {
      state.show = false;
      state.message = null;
    },
  },
});

export const {showNotification, closeNotification} = notificationSlice.actions;

export default notificationSlice.reducer;
