import React from 'react';
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import DashboardIcon from "@mui/icons-material/Dashboard";
import ListItemText from "@mui/material/ListItemText";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import PeopleIcon from "@mui/icons-material/People";
import BarChartIcon from "@mui/icons-material/BarChart";
import LayersIcon from "@mui/icons-material/Layers";
import List from "@mui/material/List";

const NavMenu: React.FC = () => {
    return (
        <List component="nav">
            <ListItemButton>
                <ListItemIcon>
                    <DashboardIcon/>
                </ListItemIcon>
                <ListItemText primary="Dashboard"/>
            </ListItemButton>
            <ListItemButton>
                <ListItemIcon>
                    <ShoppingCartIcon/>
                </ListItemIcon>
                <ListItemText primary="Orders"/>
            </ListItemButton>
            <ListItemButton>
                <ListItemIcon>
                    <PeopleIcon/>
                </ListItemIcon>
                <ListItemText primary="Customers"/>
            </ListItemButton>
            <ListItemButton>
                <ListItemIcon>
                    <BarChartIcon/>
                </ListItemIcon>
                <ListItemText primary="Reports"/>
            </ListItemButton>
            <ListItemButton>
                <ListItemIcon>
                    <LayersIcon/>
                </ListItemIcon>
                <ListItemText primary="Integrations"/>
            </ListItemButton>
        </List>
    );
};

export default NavMenu;
