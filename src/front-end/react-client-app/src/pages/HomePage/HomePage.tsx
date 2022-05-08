import {FC, useState} from "react";
import {Box, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent} from "@mui/material";

const HomePage: FC = () => {
    const [age, setAge] = useState('');
    const handleChange = (event: SelectChangeEvent) => {
        setAge(event.target.value as string);
    };

    return <Box sx={{minWidth: 120}}>
        <FormControl fullWidth>
            <InputLabel id="demo-simple-select-label">Age</InputLabel>
            <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={age}
                label="Age"
                onChange={handleChange}
            >
                <MenuItem value={10}>Ten</MenuItem>
                <MenuItem value={20}>Twenty</MenuItem>
                <MenuItem value={30}>Thirty</MenuItem>
            </Select>
        </FormControl>
    </Box>
}

export default HomePage;