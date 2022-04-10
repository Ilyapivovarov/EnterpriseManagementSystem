import { FC } from "react";
import { NavLink } from "react-router-dom";
import { Nav, NavItem } from "reactstrap";

const NavMenu: FC = () => (
  <>
    <div>
      <p>List Based</p>
      <Nav vertical>
        <NavItem>
          <NavLink to="/">Link</NavLink>
        </NavItem>
        <NavItem>
          <NavLink to="/">Link</NavLink>
        </NavItem>
        <NavItem>
          <NavLink to="/">Another Link</NavLink>
        </NavItem>
        <NavItem>
          <NavLink to="/">Disabled Link</NavLink>
        </NavItem>
      </Nav>
    </div>
  </>
);

export default NavMenu;
