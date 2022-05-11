import {FC, useEffect} from 'react';
import {useNavigate} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../hooks";
import {resetAuthState} from "../store/AuthReducer/AuthActionCreators";

const RequireAuth: FC = (props) => {
    const navigate = useNavigate();
    const {currentSession} = useAppSelector(x => x.authReducer)

    const dispatch = useAppDispatch()

    useEffect(() => {
        dispatch(resetAuthState())
        if (!currentSession)
            navigate("/sign-in")
    }, [dispatch]);

    if (currentSession)
        return (
            <>
                {props.children}
            </>
        );

    return <></>
};

export default RequireAuth;
