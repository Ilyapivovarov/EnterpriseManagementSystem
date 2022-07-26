import {FC, useState} from 'react';
import Avatar from '@mui/material/Avatar';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import ListItemText from '@mui/material/ListItemText';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import PersonIcon from '@mui/icons-material/Person';
import {FormControl, InputLabel, MenuItem, Select, Tooltip} from '@mui/material';

const emails = ['username@gmail.com', 'user02@gmail.com'];

interface UserSelectDialogProps {
    open: boolean;
    selectedValue: string;
    onClose: (value: string) => void;
}

const UserSelectDialog: FC<UserSelectDialogProps> = ({onClose, selectedValue, open}) => {
    const handleClose = () => {
        onClose(selectedValue);
    };

    const handleListItemClick = (value: string) => {
        onClose(value);
    };

    return (
        <Dialog onClose={handleClose} open={open}>
            <DialogTitle>Set executor</DialogTitle>
            <List sx={{pt: 0}}>
                {emails.map((email) => (
                    <ListItem button={true} onClick={() => handleListItemClick(email)} key={email}>
                        <ListItemAvatar>
                            <Avatar>
                                <PersonIcon/>
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemText primary={email}/>
                    </ListItem>
                ))}
            </List>
        </Dialog>
    );
};

const SimpleDialogDemo: FC = () => {
    const [open, setOpen] = useState(false);
    const [selectedValue, setSelectedValue] = useState(emails[1]);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = (value: string) => {
        setOpen(false);
        setSelectedValue(value);
    };

    return (
        <Tooltip title={"Change executor"}>
            <div>
                <FormControl variant="standard" sx={{m: 1, minWidth: 200}}>
                    <InputLabel id="task-executor-select">Executor</InputLabel>
                    <Select labelId="task-executor-select"
                            id="select-executor"
                            value={selectedValue}
                            onClick={handleClickOpen}
                            open={false}>
                        <MenuItem value={selectedValue}>
                            {selectedValue}
                        </MenuItem>
                    </Select>

                </FormControl>
                <UserSelectDialog
                    selectedValue={selectedValue}
                    open={open}
                    onClose={handleClose}
                />
            </div>
        </Tooltip>
    );
}
export default SimpleDialogDemo;