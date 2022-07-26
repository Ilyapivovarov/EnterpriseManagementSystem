import {FC, useEffect} from 'react';
import {Outlet, useNavigate} from "react-router-dom";
import {useAppDispatch} from "../hooks";
import {resetAuthState} from "../store/AuthReducer/AuthActionCreators";

const RequireAnonymous: FC = (props) => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch()

    useEffect(() => {
        dispatch(resetAuthState())
            .unwrap()
            .then(_ => {
                navigate("/")
            })
    }, []);

    return (
        <>
            <Outlet/>
        </>
    );

};

export default RequireAnonymous;
