import {Outlet} from "react-router-dom";
import {Container} from "reactstrap";
import NavMenu from "../nav";

const Layout: React.FC = () => (
    <>
        <NavMenu/>
        <Container fluid>
            <Outlet/>
        </Container>
    </>
);

export default Layout;
