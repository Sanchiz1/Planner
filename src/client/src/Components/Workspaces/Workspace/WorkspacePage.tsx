import { Breadcrumbs, Button, Link, Typography } from '@mui/material';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import { useEffect } from 'react';
import { useNavigate, useParams, Link as RouterLink } from 'react-router-dom';
import { getWorkspace, getWorkspaceMembership } from '../../../features/workspace/workspaceSlice';
import { useAppDispatch, useAppSelector } from '../../../store';

export default function WorkspacePage() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { workspaceId } = useParams();
  const workspace = useAppSelector(state => state.workspace);
  const workspaceMembership = useAppSelector(state => state.workspaceMembership);

  useEffect(() => {
    if (!workspaceId) return;
    dispatch(getWorkspace(parseInt(workspaceId)));
    dispatch(getWorkspaceMembership(parseInt(workspaceId)));
  }, [workspaceId]);

  const HandleMembersClick = () => {
    navigate("members");
  }

  return (
    <Container component="main" maxWidth='xl' sx={{
      pt: 8, pb: 6,
      bgcolor: 'background.default'
    }}>
      {workspace.error &&
        <Typography
          component="h1"
          variant="h2"
          align="left"
          color="text.primary"
          gutterBottom
        >
          {workspace.error}
        </Typography>}
      {workspace.workspace &&
        <>
          <Typography
            component="h1"
            variant="h2"
            align="left"
            color="text.primary"
            gutterBottom
          >
            {workspace.workspace.name}
          </Typography>
          <Box>
            {workspaceMembership.membership?.role.name === "Owner" &&
              <Button variant='contained' sx={{ mr: "5px" }}>Settings</Button>
            }
            <Button variant='contained' onClick={HandleMembersClick}>Members</Button>
          </Box>
        </>
      }
    </Container >
  );
}