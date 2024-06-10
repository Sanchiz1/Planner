import React from 'react';
import Toolbar from '@mui/material/Toolbar';
import { Outlet, useLocation, useNavigate, Link as RouterLink, ScrollRestoration } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import { AppBar, Box, IconButton, Theme } from '@mui/material';
import Brightness4Icon from '@mui/icons-material/Brightness4';
import Brightness7Icon from '@mui/icons-material/Brightness7';
import { useTheme } from '@emotion/react';


export default function Header() {
    const navigate = useNavigate();
    const location = useLocation();

    const theme = useTheme();

    return (
        <>
            <AppBar position='sticky' color='default'>
                <Toolbar>
                    <Typography component={RouterLink} to='/' variant='h5' sx={{ mr: 3 }}>
                        Planner
                    </Typography>
                    <Box sx={{ mr: 'auto' }}>
                        <Typography component={RouterLink} to='/Workspaces'
                            color="text.primary" sx={{ mr: 3 }}>
                            WORKSPACES
                        </Typography>
                    </Box>
                    <Typography variant="button" component={RouterLink} to='/Login'
                        color="text.primary">
                        Login
                    </Typography>
                </Toolbar>
            </AppBar>
            <Outlet />
            <ScrollRestoration />
        </>
    );
}