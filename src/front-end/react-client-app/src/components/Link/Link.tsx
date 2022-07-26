import {FC} from 'react';
import {NavLink} from "react-router-dom";

import "./Link.css"

interface LinkProps {
    to: string
}

const Link: FC<LinkProps> = ({to, children}) => {
    return (
        <NavLink id={"nav-link"} to={to}>{children}</NavLink>
    );
};

export default Link;
