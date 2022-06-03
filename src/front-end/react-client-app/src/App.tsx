import {FC} from "react";
import {Route, Routes} from "react-router-dom";
import SignInPage from "./pages/SignInPage";
import HomePage from "./pages/HomePage";
import SignUpPage from "./pages/SignUpPage";
import Layout from "./components/Layout/Layout";
import RequireAuth from "./hoc/RequireAuth";
import TaskPage from "./pages/TaskPage/inde";

const App: FC = () => (
    <Routes>
        <Route path={"/"} element={<RequireAuth children={<Layout/>}/>}>
            <Route index element={<HomePage/>}/>
            <Route path={"task/"} element={<TaskPage/>}/>
        </Route>
        <Route path={"/"}>
            <Route path={"sign-in"} element={<SignInPage/>}/>
            <Route path={"sign-up"} element={<SignUpPage/>}/>
        </Route>
    </Routes>
)

export default App;
