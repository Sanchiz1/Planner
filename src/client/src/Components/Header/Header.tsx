import { AppBar, Box } from '@mui/material';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import { useEffect } from 'react';
import { Outlet, Link as RouterLink, ScrollRestoration, useLocation, useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../store';
import { getUser } from '../../features/account/accountSlice';


export default function Header() {
    const navigate = useNavigate();
    const location = useLocation();
    const dispatch = useAppDispatch();

    const user = useAppSelector(store => store.account.user);

    useEffect(() => {
        dispatch(getUser());
    }, [])

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
                    {user ?

                        <Typography variant="button" component={RouterLink} to='/Account'
                            color="text.primary">
                            {user.displayName}
                        </Typography> :
                        <Typography variant="button" component={RouterLink} to='/Login'
                            color="text.primary">
                            Login
                        </Typography>
                    }
                </Toolbar>
            </AppBar>
            <Outlet />
            <ScrollRestoration />
        </>
    );
}