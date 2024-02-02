import React from 'react';
import Toolbar from '@mui/material/Toolbar';
import { Outlet, useLocation, useNavigate, Link as RouterLink, ScrollRestoration } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import { AppBar, Box } from '@mui/material';

export default function Header() {
    const navigate = useNavigate();
    const location = useLocation();

    return (
        <>
            <AppBar position='sticky' color='default'>
                <Toolbar>
                    <Typography component={RouterLink} to='/' variant='h5' sx={{ mr: 'auto' }}>
                        Planner
                    </Typography>
                    <Typography variant="button" component={RouterLink} to='/Tasks'
                        color="text.primary">
                        Tasks
                    </Typography>
                </Toolbar>
            </AppBar>
            <Outlet />
            <ScrollRestoration />
        </>
    );
}