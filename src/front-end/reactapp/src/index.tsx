import {createRoot} from 'react-dom/client';
import {Provider} from 'react-redux';
import {BrowserRouter} from 'react-router-dom';
import {store} from './store';
import App from './App';
import {firstResetAuthState} from './store/AuthReducer/AuthActionCreators';

const container = document.getElementById('app');
const root = createRoot(container!);

store.dispatch(firstResetAuthState());
root.render(
    <Provider store={store}>
      <BrowserRouter>
        <App/>
      </BrowserRouter>
    </Provider>,
);
