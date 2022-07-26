import React, {FC} from 'react';
import {TaskStatusDto} from "../../types/taskTypes";
import {FormControl, InputLabel, MenuItem, Select, Tooltip} from "@mui/material";

const mockTaskStatues: TaskStatusDto[] = [
    {
        id: 1,
        name: "Registered",
        guid: "84d8a7a6-c2be-41c4-90e1-36a7969ab3f1",
    },
    {
        id: 2,
        name: "Active",
        guid: "84d8a7a6-c2be-41c4-90e1-36a7969ab3f1",
    },
    {
        id: 3,
        name: "Complete",
        guid: "84d8a7a6-c2be-41c4-90e1-36a7969ab3f1",
    }]

interface TaskStatusSelectorProps {
    selectedStatusId: number
}

const TaskStatusSelector: FC<TaskStatusSelectorProps> = ({selectedStatusId}) => {

    return (<Tooltip title={"Change status"}>
        <FormControl variant="standard" sx={{m: 1, minWidth: 120}}>
            <InputLabel id="task-status-select">Status</InputLabel>
            <Select
                labelId="task-status-select"
                id="select-status"
                value={selectedStatusId}
            >
                {mockTaskStatues.map(x => <MenuItem key={x.id} value={x.id}>{x.name}</MenuItem>)}
            </Select>
        </FormControl>
    </Tooltip>);
};

export default TaskStatusSelector;
