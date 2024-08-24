import { Typography } from '@mui/material';
import Container from '@mui/material/Container';
import { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { deleteWorkspaceWait, getWorkspace, updateWorkspaceWait } from '../../../features/workspace/workspaceSlice';
import { getWorkspaceMembership } from '../../../features/workspaceMembership/workspaceMembershipSlice';
import { ShowFailure, ShowSuccess } from '../../../Helpers/SnackBarHelper';
import { useAppDispatch, useAppSelector } from '../../../store';
import WorkspaceSettingsComponent from './WorkspaceSettingsComponent';
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

  useEffect(() => {
    if (workspace.updateSuccess) {
      ShowSuccess("Workspace updated successfully");
      dispatch(updateWorkspaceWait());
    }
    if (workspace.updateError) {
      ShowFailure(workspace.updateError);
      dispatch(updateWorkspaceWait());
    }
  }, [workspace.updateSuccess, workspace.updateError]);

  useEffect(() => {
    if (workspace.deleteSuccess) {
      ShowSuccess("Workspace deleted successfully");
      dispatch(deleteWorkspaceWait());
    }
    if (workspace.deleteError) {
      ShowFailure(workspace.deleteError);
      dispatch(deleteWorkspaceWait());
    }
  }, [workspace.deleteSuccess, workspace.deleteError]);

  return (
    <Container component="main" maxWidth='xl' sx={{
      pt: 8, pb: 6,
      bgcolor: 'background.default'
    }}>
      {(workspaceMembership.error || (workspaceMembership.membership?.role.name !== OWNER_ROLE_NAME && workspaceMembership.membership)) ?
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
        <>
          {
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
          {workspace.workspace && workspaceMembership.membership &&
            <WorkspaceSettingsComponent workspace={workspace.workspace} membership={workspaceMembership.membership} />
          }
        </>
      }
    </Container >
  );
}