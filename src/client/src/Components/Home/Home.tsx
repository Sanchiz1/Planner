import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';

export default function Home() {

    return (
        <Container component="main" maxWidth='xl' sx={{ pt: 8, pb: 6 }}>
            <Typography
                component="h1"
                variant="h2"
                align="center"
                color="text.primary"
                gutterBottom
            >
                Welcome to Planner
            </Typography>
            <Typography variant="h5" align="center" color="text.secondary" component="p">
                Say Goodbye to the chaos & Hello to productivity!
            </Typography>
        </Container>
    );
}