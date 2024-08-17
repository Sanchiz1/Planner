import { Typography } from '@mui/material';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import { useAppDispatch, useAppSelector } from '../../store';
import logoutAction from '../../features/auth/actions/logoutAction';
import { useNavigate } from 'react-router-dom';
import { getUser } from '../../features/account/accountSlice';

export default function AccountPage() {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    const user = useAppSelector(store => store.account.user);

    const HandleLogoutClick = () => {
        dispatch(logoutAction());
        dispatch(getUser());
        navigate("/");
    }


    return (
        <Container component="main" maxWidth='xl' sx={{
            pt: 8, pb: 6,
            bgcolor: 'background.default'
        }}>
            <Typography
                component="h1"
                variant="h2"
                align="left"
                color="text.primary"
                gutterBottom
            >
                {user?.displayName}
            </Typography>
            <Button variant='contained' onClick={HandleLogoutClick}>Logout</Button>
        </Container>
    );
}