import React, {FC} from 'react';
import ListItemIcon from "@mui/material/ListItemIcon";
import SettingsIcon from '@mui/icons-material/Settings';
import ListItemText from "@mui/material/ListItemText";
import ListItemButton from "@mui/material/ListItemButton";
import {useAppSelector} from "../../hooks";
import jwt_decode from "jwt-decode";
import {DecodeToken} from "../../types/authTypes";

const AdminNavItems: FC = () => {
    const {currentSession} = useAppSelector(x => x.authReducer);
    const decodeToken = jwt_decode<DecodeToken>(currentSession!.accessToken);
    
    if (decodeToken.role == "Admin")
        return (
            <ListItemButton>
                <ListItemIcon>
                    <SettingsIcon/>
                </ListItemIcon>
                <ListItemText primary="Settings"/>
            </ListItemButton>
        );

    return <></>
};

export default AdminNavItems;
