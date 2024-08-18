import { Breadcrumbs, Button, Link, Typography } from '@mui/material';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import { useEffect } from 'react';
import { Link as RouterLink, useParams } from 'react-router-dom';
import { getWorkspace, getWorkspaceMembership } from '../../../features/workspace/workspaceSlice';
import { useAppDispatch, useAppSelector } from '../../../store';
import MemberElement from './MemberElement';
import { getWorkspaceMembers } from '../../../features/workspaceMembers/workspaceMembersSlice';

export default function WorkspaceMembersPage() {
  const dispatch = useAppDispatch();
  const { workspaceId } = useParams();
  const workspace = useAppSelector(state => state.workspace);
  const workspaceMembers = useAppSelector(state => state.workspaceMembers);
  const workspaceMembership = useAppSelector(state => state.workspaceMembership);

  useEffect(() => {
    if (!workspaceId) return;

    const id = parseInt(workspaceId);

    dispatch(getWorkspace(id));
    dispatch(getWorkspaceMembers(id));
    dispatch(getWorkspaceMembership(id));
  }, [workspaceId]);

  return (
    <Container component="main" maxWidth='xl' sx={{
      pt: 6, pb: 6,
      bgcolor: 'background.default'
    }}>
      {workspace.error ?
        <Typography
          component="h1"
          variant="h2"
          align="left"
          color="text.primary"
          gutterBottom
        >
          {workspace.error.toString()}
        </Typography> :
        <>
          <Breadcrumbs aria-label="breadcrumb">
            <Link underline="hover" color="inherit" component={RouterLink} to={"/workspaces/" + workspace?.workspace?.id}>
              {workspace?.workspace?.name}
            </Link>
            <Typography color="text.primary">Members</Typography>
          </Breadcrumbs>
          <Box sx={{ mt: "5px" }}>
            {workspaceMembership.membership?.role.name === "Owner" &&
              <Button variant='contained' sx={{ mr: "5px" }}>Add</Button>
            }
          </Box>
          {workspaceMembers.members &&
            <Container maxWidth='md'>
              {workspaceMembers.members.map(m => <MemberElement membershipUser={m} />)}
            </Container>
          }
        </>
      }
    </Container >
  );
}