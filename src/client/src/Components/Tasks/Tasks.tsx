import { Typography } from '@mui/material';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';

export default function Tasks() {

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
        Tasks
      </Typography>
      <Button variant='contained'>Get</Button>
    </Container>
  );
}