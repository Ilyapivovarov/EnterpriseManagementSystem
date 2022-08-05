import {createSlice} from '@reduxjs/toolkit';
import type {PayloadAction} from '@reduxjs/toolkit';

interface CounterState {
  message: string | null,
  show: boolean;
}

const initialState: CounterState = {
  show: false,
  message: null,
};

export const notificationSlice = createSlice({
  name: 'notification',
  initialState,
  reducers: {
    showNotification: (state, action: PayloadAction<string>) => {
      state.show = true;
      state.message = action.payload;
    },
    closeNotification: (state) => {
      state.show = false;
      state.message = null;
    },
  },
});

export const {showNotification, closeNotification} = notificationSlice.actions;

export default notificationSlice.reducer;
