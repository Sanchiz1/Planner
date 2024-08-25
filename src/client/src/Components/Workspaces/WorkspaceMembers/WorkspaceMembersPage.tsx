import { Breadcrumbs, Button, Link, Typography } from '@mui/material';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import { useEffect } from 'react';
import { Link as RouterLink, useNavigate, useParams } from 'react-router-dom';
import { getWorkspace, updateWorkspaceWait } from '../../../features/workspace/workspaceSlice';
import { useAppDispatch, useAppSelector } from '../../../store';
import MemberElement from './MemberElement';
import { getWorkspaceMembers, removeWorkspaceMember, removeWorkspaceMemberWait, updateWorkspaceMember, updateWorkspaceMemberWait } from '../../../features/workspaceMembers/workspaceMembersSlice';
import { getWorkspaceMembership } from '../../../features/workspaceMembership/workspaceMembershipSlice';
import { OWNER_ROLE_NAME } from '../../../config';
import { ShowFailure, ShowSuccess } from '../../../Helpers/SnackBarHelper';
import { MembershipUser } from '../../../Types/MembershipUser';
import { UpdateWorkspaceMemberRole } from '../../../API/WorkspaceRequests';

export default function WorkspaceMembersPage() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
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

  useEffect(() => {
    if (workspaceMembers.removeSuccess) {
      ShowSuccess("Member removed successfully");
    };
    if (workspaceMembers.removeError) {
      ShowFailure(workspaceMembers.removeError);
    };
    dispatch(removeWorkspaceMemberWait());
  }, [workspaceMembers.removeSuccess, workspaceMembers.removeError]);

  useEffect(() => {
    if (workspaceMembers.updateSuccess) {
      ShowSuccess("Member updated successfully");
    };
    if (workspaceMembers.updateError) {
      ShowFailure(workspaceMembers.updateError);
    };
    dispatch(updateWorkspaceMemberWait());
  }, [workspaceMembers.updateSuccess, workspaceMembers.updateError]);

  const HandleRemoveMember = (membershipId: number) => {
    if (workspace.workspace?.id && membershipId) {
      dispatch(removeWorkspaceMember({
        workspaceId: workspace.workspace?.id,
        ToRemoveMembershipId: membershipId
      }));
    }
  }

  const HandleUpdateMember = (membershipId: number, roleId: number) => {
    if (workspace.workspace?.id && membershipId && roleId) {
      dispatch(updateWorkspaceMember({
        workspaceId: workspace.workspace?.id,
        ToUpdateMembershipId: membershipId,
        ToUpdateRoleId: roleId
      }));
    }
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
      {workspaceMembers.members &&
        <>
          <Breadcrumbs aria-label="breadcrumb" sx={{ mb: 2 }}>
            <Link underline="hover" color="inherit" component={RouterLink} to={"/workspaces/" + workspace?.workspace?.id}>
              {workspace?.workspace?.name}
            </Link>
            <Typography color="text.primary">Members</Typography>
          </Breadcrumbs>
          <Box sx={{ mt: "5px" }}>
            {workspaceMembership.membership?.role.name === OWNER_ROLE_NAME &&
              <Button variant='contained' sx={{ mr: "5px" }} onClick={() => navigate("add")}>Add</Button>
            }
          </Box>
          {workspaceMembers.members &&
            <Container maxWidth='md'>
              {workspaceMembers.members.map(m =>
                <MemberElement
                  membership={workspaceMembership.membership}
                  membershipUser={m}
                  onUpdateMember={(roleId: number) => HandleUpdateMember(m.membership.id, roleId)}
                  onRemoveMember={() => HandleRemoveMember(m.membership.id)}
                />)}
            </Container>
          }
        </>
      }
    </Container >
  );
}