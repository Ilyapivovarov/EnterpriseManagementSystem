import {FC, useEffect} from "react";
import {Box, CircularProgress, Paper, Typography} from "@mui/material";
import {useGetAccountByGuidQuery} from "../../services/accountService";
import {useAppSelector} from "../../hooks";
import {Session} from "../../types/authTypes";

const HomePage: FC = () => {
    const {currentSession} = useAppSelector(x => x.authReducer);
    const {data, isLoading, isSuccess} = useGetAccountByGuidQuery(currentSession!.userGuid);
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