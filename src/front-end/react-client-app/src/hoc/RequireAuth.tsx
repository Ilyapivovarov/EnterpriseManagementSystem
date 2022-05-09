import {FC, useEffect} from 'react';
import {useNavigate} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../hooks";
import {resetAuthState} from "../store/AuthReducer/AuthActionCreators";


const RequireAuth: FC = (props) => {
        const navigate = useNavigate();
        const {isAuth, currentSession} = useAppSelector(x => x.authReducer)

        const dispatch = useAppDispatch()
        dispatch(resetAuthState())
    
        useEffect(() => {
            if (!isAuth)
                navigate("/sign-in")
        }, [isAuth, currentSession]);
        
        if (isAuth)
            return (
                <>
                    {props.children}
                </>
            );

        return <></>
    }
;

export default RequireAuth;
