import {FC, useEffect} from 'react';
import {useNavigate} from "react-router-dom";

const RequireAuth: FC = (props) => {

    const navigate = useNavigate();
    const isAuth = false;
    useEffect(() => {
        if (!isAuth)
            navigate("/sign-in");
    }, [isAuth, navigate]);

    return (
        <>
            {props.children}
        </>
    );
};

export default RequireAuth;
