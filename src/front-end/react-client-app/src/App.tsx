import { FC } from "react";
import { Routes, Route } from "react-router-dom";
import Layout from "./components/layout";
import HomePage from "./pages/home/";

const App: FC = () => (
  <Routes>
    <Route path={"/"} element={<Layout />}>
      <Route index element={<HomePage />} />
    </Route>
  </Routes>
);

export default App;
