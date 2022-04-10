import { Outlet } from "react-router-dom";
import { Container } from "reactstrap";

const Layout: React.FC = () => (
    <Container>
        <Outlet />
    </Container>
);

export default Layout;
