import { Google } from '@mui/icons-material';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Avatar from '@mui/material/Avatar';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import Link from '@mui/material/Link';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { ShowFailure } from '../../Helpers/SnackBarHelper';
import { login } from '../../features/auth/authSlice';
import { useAppDispatch, useAppSelector } from '../../store';
import { getUser } from '../../features/account/accountSlice';

function Copyright(props: any) {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'Copyright © '}
            <Link color="inherit" href="https://github.com/Sanchiz1">
                Sanchiz
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

export default function Login() {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const { error, success } = useAppSelector(state => state.auth);

    const handleGoogleSubmit = () => {
        window.location.href = "https://localhost:7269/Account/googlelogin";
    }

    useEffect(() => {
        if (success) {
            navigate("/");
            dispatch(getUser());
        }
        if (error) ShowFailure(error);
    }, [success])

    const handleLoginSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        dispatch(login({
            email: data.get('email')?.toString()!,
            password: data.get('password')?.toString()!,
        }));
    };

    return (
        <Container component="main" maxWidth='xs' sx={{ pt: 8 }}
        >
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    justifyContent: 'center'
                }}
            >
                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Login
                </Typography>
                <Box component="form" onSubmit={handleLoginSubmit} noValidate sx={{ mt: 1 }}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="email"
                        label="Email Address"
                        name="email"
                        autoComplete="none"
                        color='secondary'
                        autoFocus
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        name="password"
                        label="Password"
                        type="password"
                        id="password"
                        color='secondary'
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color='secondary'
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Sign In
                    </Button>
                    <Grid container>
                        <Grid item xs>
                            <Link href="#" variant="body2">
                                Forgot password?
                            </Link>
                        </Grid>
                        <Grid item>
                            <Link href="#" variant="body2">
                                {"Don't have an account? Sign Up"}
                            </Link>
                        </Grid>
                    </Grid>
                    <Button
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                        color='secondary'
                        startIcon={<Google />}
                        onClick={handleGoogleSubmit}
                    >
                        Sign In with google
                    </Button>
                </Box>
            </Box>
            <Copyright sx={{ mt: 8 }} />
        </Container>
    );
}