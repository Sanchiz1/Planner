import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { Card, CardActions, CardContent, Typography } from '@mui/material';
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