import React, {FC} from 'react';
import ListItemIcon from "@mui/material/ListItemIcon";
import SettingsIcon from '@mui/icons-material/Settings';
import ListItemText from "@mui/material/ListItemText";
import ListItemButton from "@mui/material/ListItemButton";
import {useAppSelector} from "../../hooks";
import jwt_decode from "jwt-decode";

interface DecodeToken {
    "email": string,
    "sub": string,
    "role": string,
    "exp": number,
    "iss": string,
}

const AdminNavItems: FC = () => {

    const {currentSession} = useAppSelector(x => x.authReducer);

    const decodeToken = jwt_decode<DecodeToken>(currentSession!.accessToken);
    console.log(decodeToken)
    return (
        <ListItemButton>
            <ListItemIcon>
                <SettingsIcon/>
            </ListItemIcon>
            <ListItemText primary="Settings"/>
        </ListItemButton>
    );
};

export default AdminNavItems;
