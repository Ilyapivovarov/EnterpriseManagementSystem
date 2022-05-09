import { createRoot } from 'react-dom/client';
import {StrictMode} from "react";
import {Provider} from "react-redux";
import {BrowserRouter} from "react-router-dom";
import {store} from "./store";
import App from "./App";

const container = document.getElementById('app');
const root = createRoot(container!);
root.render(
    <StrictMode>
        <Provider store={store}>
            <BrowserRouter>
                <App/>
            </BrowserRouter>
        </Provider>
    </StrictMode>
);
