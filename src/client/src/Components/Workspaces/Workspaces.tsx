import { Typography } from '@mui/material';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import { Workspace } from '../../Types/Workspace';
import WorkspaceElement from './WorkspaceElement';

export default function Workspaces() {
  const workspaces: Workspace[] = [
    {
      id: 1,
      title: "New workspace"
    },
    {
      id: 1,
      title: "New workspace"
    },
    {
      id: 1,
      title: "New workspace"
    }
  ]

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
        Workspaces
      </Typography>
      <Button variant='contained'>Create</Button>
      <Box sx={{
        mt: "10px",
        display: "flex",
        flexWrap: "wrap"
      }}>
        {
          workspaces.map(w =>
            <WorkspaceElement workspace={w} key={w.id}></WorkspaceElement>
          )
        }
      </Box>
    </Container>
  );
}