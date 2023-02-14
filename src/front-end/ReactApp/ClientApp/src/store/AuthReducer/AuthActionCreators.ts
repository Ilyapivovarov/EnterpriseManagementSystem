import {createAsyncThunk} from '@reduxjs/toolkit';
import {DecodeToken, Session, SignIn, SignUp} from '../../types/authTypes';
import jwtDecode from 'jwt-decode';

const baseUrl = process.env.REACT_APP_API_KEY;

export const resetAuthState = createAsyncThunk<Session, void, { rejectValue: string }>(
    'authSlice/reset-auth-state',
    async (_, {rejectWithValue}) => {
      console.log('resetAuthState');
      const session = JSON.parse(localStorage.getItem('session')!) as Session | null;
      if (!session) {
        console.log('Not found token in localstorage');
        return rejectWithValue('Not found token in localstorage');
      }

      const decodeToken = jwtDecode<DecodeToken>(session.accessToken);
      if (new Date(decodeToken.exp * 1000 - 20000) > new Date()) {
        console.log('Token is valid');
        return session;
      }

      console.log('Trying update token');
      const newSession = await tryUpdateToken(session);
      if (newSession) {
        return session;
      }

      console.log('Error while updating token. Remove item from localstorage by key "session"');
      localStorage.removeItem('session');
      return rejectWithValue('Error while updating token');
    },
);

export const signIn = createAsyncThunk<Session, SignIn, { rejectValue: string }>(
    'authSlice/sing-in',
    async (authModel, {rejectWithValue}) => {
      const response = await fetch(`${baseUrl}/auth/sign-in`, {
        method: 'POST',
        headers: {
          'content-type': 'application/json;charset=UTF-8',
        },
        body: JSON.stringify(authModel),
      });
      if (response.ok) {
        const result = await response.json();
        localStorage.setItem('session', JSON.stringify(result));
        return result;
      }

      return rejectWithValue(await response.text());
    },
);

export const signUp = createAsyncThunk<Session, SignUp, { rejectValue: string }>(
    'authSlice/sing-up',
    async (authModel, {rejectWithValue}) => {
      try {
        console.log(authModel);
        const response = await fetch(`${baseUrl}/auth/sign-up`, {
          method: 'POST',
          headers: {
            'content-type': 'application/json;charset=UTF-8',
          },
          body: JSON.stringify(authModel),
        });

        if (response.ok) {
          const result = await response.json();
          localStorage.setItem('session', JSON.stringify(result));
          return result;
        }

        return rejectWithValue(await response.text());
      } catch (e) {
        console.log(e);
        rejectWithValue('Error');
      }
    },
);

export const signOut = createAsyncThunk<void, void>(
    'authSlice/sign-out',
    async () => {
      console.log('sign out');
      localStorage.clear();
    },
);


async function tryUpdateToken(oldSession: Session ) : Promise<Session | null> {
  try {
    const response = await fetch(`${baseUrl}/auth/refresh/`, {
      method: 'PUT',
      body: JSON.stringify({refreshToken: oldSession.refreshToken}),
      headers: {
        'content-type': 'application/json;charset=UTF-8',
      },
    });

    if (response.ok) {
      const result = await response.json();
      console.log('Token successfully updated');
      localStorage.setItem('session', JSON.stringify(result));
      return result as Session;
    }
    return null;
  } catch (e) {
    console.log(e);
    return null;
  }
}


