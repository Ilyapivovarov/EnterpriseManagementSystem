import {FC} from "react";
import {Route, Routes} from "react-router-dom";
import SignInPage from "./pages/SignInPage/SignInPage";
import HomePage from "./pages/HomePage/HomePage";
import SignUpPage from "./pages/SignUpPage/SignUpPage";

const App: FC = () => (
    <Routes>
        <Route path={"/"}>
            <Route index element={<HomePage/>}/>
        </Route>
        <Route path={"/"}>
            <Route path={"sign-in"} element={<SignInPage/>}/>
            <Route path={"sign-up"} element={<SignUpPage/>}/>
        </Route>
    </Routes>
);

export default App;
