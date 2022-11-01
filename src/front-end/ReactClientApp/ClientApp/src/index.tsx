import {createRoot} from 'react-dom/client';
import {Provider} from 'react-redux';
import {BrowserRouter} from 'react-router-dom';
import {store} from './store';
import App from './App';
import {firstResetAuthState} from './store/AuthReducer/AuthActionCreators';
import registerServiceWorker from './registerServiceWorker';

const container = document.getElementById('root');
const root = createRoot(container!);
console.log(process.env.NODE_ENV);
store.dispatch(firstResetAuthState());
root.render(
    <Provider store={store}>
      <BrowserRouter>
        <App/>
      </BrowserRouter>
    </Provider>,
);

registerServiceWorker();
