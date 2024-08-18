import { Typography } from '@mui/material';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import { useEffect } from 'react';
import { getMyWorkspaces } from '../../../features/myWorkspaces/myWorkspacesSlice';
import { ShowFailure } from '../../../Helpers/SnackBarHelper';
import { useAppDispatch, useAppSelector } from '../../../store';
import MemershipWorkspaceElementProps from './MembershipWorkspaceElement';

export default function MyWorkspacesPage() {
  const dispatch = useAppDispatch();
  const { error, success, memerships } = useAppSelector(state => state.myWorkspaces);

  useEffect(() => {
    dispatch(getMyWorkspaces())
  }, []);

  useEffect(() => {
    if (error) ShowFailure(error);
  }, [success]);

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
          memerships &&
          memerships.map(m =>
            <MemershipWorkspaceElementProps membership={m} key={m.id}></MemershipWorkspaceElementProps>
          )
        }
      </Box>
    </Container>
  );
}