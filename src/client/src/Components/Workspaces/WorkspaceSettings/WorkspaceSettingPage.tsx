import { Breadcrumbs, FormControl, Link, TextField, Typography } from '@mui/material';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import { useEffect } from 'react';
import { Link as RouterLink, useParams } from 'react-router-dom';
import { getWorkspace } from '../../../features/workspace/workspaceSlice';
import { getWorkspaceMembers } from '../../../features/workspaceMembers/workspaceMembersSlice';
import { useAppDispatch, useAppSelector } from '../../../store';
import WorkspaceSettingsComponent from './WorkspaceSettingsComponent';
import { getWorkspaceMembership } from '../../../features/workspaceMembership/workspaceMembershipSlice';
import { OWNER_ROLE_NAME } from '../../../config';

export default function WorkspaceSettingsPage() {
  const dispatch = useAppDispatch();
  const { workspaceId } = useParams();
  const workspace = useAppSelector(state => state.workspace);
  const workspaceMembership = useAppSelector(state => state.workspaceMembership);

  useEffect(() => {
    if (!workspaceId) return;

    const id = parseInt(workspaceId);

    dispatch(getWorkspace(id));
    dispatch(getWorkspaceMembership(id));
  }, [workspaceId]);

  return (
    <Container component="main" maxWidth='xl' sx={{
      pt: 6, pb: 6,
      bgcolor: 'background.default'
    }}>
      {workspaceMembership.error ?
        <Typography
          component="h1"
          variant="h2"
          align="left"
          color="text.primary"
          gutterBottom
        >
          Permission denied
        </Typography>
        :
        <>{
          workspace.error &&
          <Typography
            component="h1"
            variant="h2"
            align="left"
            color="text.primary"
            gutterBottom
          >
            {workspace.error}
          </Typography>
        }
          {workspace.workspace &&
            <WorkspaceSettingsComponent workspace={workspace.workspace} membership={workspaceMembership.membership} />
          }
        </>
      }
    </Container >
  );
}