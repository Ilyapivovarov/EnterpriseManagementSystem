import React, {FC} from 'react';
import {useAppSelector} from "../../hooks";
import jwt_decode from "jwt-decode";
import {DecodeToken} from "../../types/authTypes";
import {useNavigate} from "react-router-dom";
import {Paper, Typography} from "@mui/material";

const SettingsPage: FC = () => {
    const {currentSession} = useAppSelector(x => x.authReducer);
    const navigate = useNavigate();

    const decodeToken = jwt_decode<DecodeToken>(currentSession!.accessToken);
    if (decodeToken.role != "Admin")
        navigate("/")

    return <Paper
        sx={{
            p: 2,
            display: 'flex',
            flexDirection: 'column',
            height: '100%',
        }}
    >
        <Typography variant="h3" gutterBottom component="div">
            There will be settings page
        </Typography>

    </Paper>
};

export default SettingsPage;
