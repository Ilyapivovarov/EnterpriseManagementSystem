import React from 'react';
import {Paper, TextField, Typography} from "@mui/material";

const TaskPage = () => {
    return (
        <Paper
            sx={{
                p: 2,
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
            }}
        >
            <Typography variant="h3" gutterBottom component="div">
                h3. Heading
            </Typography>

            <Typography variant="body1" gutterBottom>
                body1. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quos
                blanditiis tenetur unde suscipit, quam beatae rerum inventore consectetur,
                neque doloribus, cupiditate numquam dignissimos laborum fugiat deleniti? Eum
                quasi quidem quibusdam.
            </Typography>
            <div/>
            <TextField
                id="outlined-multiline-static"
                multiline
                rows={4}
                defaultValue="Default Value"
            />
        </Paper>)
};

export default TaskPage;
