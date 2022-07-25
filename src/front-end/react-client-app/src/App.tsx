import {FC} from "react";
import {Route, Routes} from "react-router-dom";
import SignInPage from "./pages/SignInPage";
import HomePage from "./pages/HomePage";
import SignUpPage from "./pages/SignUpPage";
import Layout from "./components/Layout/Layout";
import RequireAuth from "./hoc/RequireAuth";
import TaskPage from "./pages/TaskPage";
import SettingsPage from "./pages/SettingsPage";
import RequireAnonymous from "./hoc/RequireAnonymous";

const App: FC = () => (
    <Routes>
        <Route path={"/"} element={<RequireAuth children={<Layout/>}/>}>
            <Route index element={<HomePage/>}/>
            <Route path={"task/"} element={<TaskPage/>}/>
            <Route path={"settings/"} element={<SettingsPage/>}/>
        </Route>
        <Route path={"/"} element={<RequireAnonymous/>}>
            <Route path={"sign-in"} element={<SignInPage/>}/>
            <Route path={"sign-up"} element={<SignUpPage/>}/>
        </Route>
    </Routes>
)

export default App;

const NotFoundPage: FC = () => (
    <h1>Not found</h1>
)
