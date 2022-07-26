import React from 'react';
import {Box, Button, ButtonGroup, Paper, Typography} from "@mui/material";
import {NavLink} from "react-router-dom";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

interface UserDto {
    guid: string,
    firstName: string,
    lastName: string,
    emailAddress: string,
}

interface TaskStatusDto {
    name: string
}

interface TaskDto {
    id: number,
    guid: string,
    name: string,
    description: string,
    executor: UserDto,
    observers: UserDto[],
    inspector: UserDto,
    author: UserDto,
    status: TaskStatusDto,
}

const mockData: TaskDto = {
    id: 1,
    guid: "ca851edd-cafd-4ce2-9daa-1ee323502f96",
    name: "Test task",
    description: "Show task",
    executor: {
        emailAddress: "admin@admin.com",
        firstName: "Admin",
        lastName: "ADmin",
        guid: "036e58dc-17c3-4c60-93a8-572d6089e3f1"
    },
    author: {
        emailAddress: "admin@admin.com",
        firstName: "Admin",
        lastName: "ADmin",
        guid: "036e58dc-17c3-4c60-93a8-572d6089e3f1"
    },

    inspector: {
        emailAddress: "admin@admin.com",
        firstName: "Admin",
        lastName: "ADmin",
        guid: "036e58dc-17c3-4c60-93a8-572d6089e3f1"
    },
    observers: [
        {
            emailAddress: "admin@admin.com",
            firstName: "Admin",
            lastName: "ADmin",
            guid: "036e58dc-17c3-4c60-93a8-572d6089e3f1"
        }
    ],
    status: {
        name: "Active"
    }
}


const TaskPage = () => {


    return (
        <Paper
            sx={{
                padding: 2,
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
            }}>


            <Box padding={1}>
                <ButtonGroup size="small">
                    <Button key="one"><EditIcon/></Button>
                    <Button key="one"><DeleteIcon/></Button>
                </ButtonGroup>
            </Box>
            <Typography fontSize={14} paddingLeft={1}>
                Task-{mockData.id} created by <NavLink
                to={`/users/${mockData.author.guid}`}>{mockData.author.firstName} {mockData.author.lastName} </NavLink>
            </Typography>


        </Paper>)
};

export default TaskPage;
