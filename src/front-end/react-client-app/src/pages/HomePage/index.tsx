import {FC} from "react";
import {Paper, Typography} from "@mui/material";
import {useSignInMutation} from "../../services/authService";

const HomePage: FC = () => {
    useSignInMutation()
    return (
        <Paper
            sx={{
                p: 2,
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
            }}
        >
            <Typography>
                Home Page
            </Typography>
        </Paper>
    )
}

export default HomePage;