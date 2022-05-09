import {FC} from "react";
import {Box, CircularProgress, Paper, Typography} from "@mui/material";
import {useGetAccountByGuidQuery} from "../../services/accountService";
import {Session} from "../../types/authTypes";

const HomePage: FC = () => {
    const session = JSON.parse(localStorage.getItem("session")!) as Session;
    const {data, isLoading, isSuccess} = useGetAccountByGuidQuery(session.userGuid);

    return (
        <Paper
            sx={{
                p: 2,
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
            }}
        >
            {isLoading && <Box sx={{display: 'flex'}}>
                <CircularProgress/>
            </Box>}
            {isSuccess && <Typography> Welcome {data.firstName}</Typography>}
        </Paper>
    )
}

export default HomePage;