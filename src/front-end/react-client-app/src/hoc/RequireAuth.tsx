import {FC, useEffect} from 'react';
import {useNavigate} from "react-router-dom";

const RequireAuth: FC = (props) => {
    const navigate = useNavigate();
    const session = localStorage.getItem("session");
    
    useEffect(() => {
        if (!session)
            navigate("/sign-in");
    }, []);

    return (
        <>
            {props.children}
        </>
    );
};

export default RequireAuth;
