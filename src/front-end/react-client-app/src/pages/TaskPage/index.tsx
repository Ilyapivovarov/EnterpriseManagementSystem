import React from 'react';
import {Paper, Typography} from "@mui/material";

const TaskPage = () => {
    return (<div style={{display: "flex"}}>
        <Paper
            sx={{
                padding: 2,
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
            }}>
            <Paper sx={{
                display: 'flex',
                m: 2,
                p: 1,
                flexDirection: 'column',
            }}>
                <Typography variant="h5" gutterBottom component="div">
                    Task name
                </Typography>
            </Paper>


            <Paper sx={{
                m: 2,
                p: 1,
                display: 'flex',
                flexDirection: 'column',
            }}>
                <Typography gutterBottom component="div">
                    Task description
                </Typography>
            </Paper>
        </Paper>
        <Paper sx={{
            display: 'flex',
            m: 2,
            p: 1,
            flexDirection: 'column',
        }}>
            <Typography gutterBottom component="div">
                Task description
            </Typography>
        </Paper>
    </div>)
};

export default TaskPage;
