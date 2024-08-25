import { Breadcrumbs, Button, ButtonGroup, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FormControl, InputLabel, Link, MenuItem, Select, TextField, Typography } from '@mui/material';
import Container from '@mui/material/Container';
import { useEffect, useState } from 'react';
import { Link as RouterLink, useParams } from 'react-router-dom';
import { MEMBER_ROLE_ID, MEMBER_ROLE_NAME, OWNER_ROLE_ID, OWNER_ROLE_NAME, VIEWER_ROLE_ID, VIEWER_ROLE_NAME } from '../../../config';
import { getWorkspace } from '../../../features/workspace/workspaceSlice';
import { getWorkspaceMembers } from '../../../features/workspaceMembers/workspaceMembersSlice';
import { getWorkspaceMembership } from '../../../features/workspaceMembership/workspaceMembershipSlice';
import { useAppDispatch, useAppSelector } from '../../../store';
import { addWorkspaceMember, addWorkspaceMemberWait, getUsers } from '../../../features/addWorkspaceMember/usersReducerSlice';
import UserElement from './UserElement';
import { User } from '../../../Types/User';
import { ShowFailure, ShowSuccess } from '../../../Helpers/SnackBarHelper';

export default function AddWorkspaceMemberPage() {
    const dispatch = useAppDispatch();
    const { workspaceId } = useParams();
    const workspace = useAppSelector(state => state.workspace);
    const workspaceMembership = useAppSelector(state => state.workspaceMembership);
    const users = useAppSelector(state => state.users);

    const [search, setSearch] = useState<string>("");
    const [toAddUser, setToAddUser] = useState<User | null>(null);
    const [toAddUserRole, setToAddUserRole] = useState<number>(2);

    useEffect(() => {
        if (!workspaceId) return;

        const id = parseInt(workspaceId);

        dispatch(getWorkspace(id));
        dispatch(getWorkspaceMembership(id));
    }, [workspaceId]);

    useEffect(() => {
        if (users.addSuccess) {
            ShowSuccess("User added successfully");
            dispatch(addWorkspaceMemberWait());
        }
        if (users.addError) {
            ShowFailure(users.addError);
            dispatch(addWorkspaceMemberWait());
        }
    }, [users.addSuccess, users.addError]);

    const HandleSearchUsers = () => {
        if (search.length === 0) return;

        dispatch(getUsers(search));
    }

    const handleAddUserDialogOpen = (user: User) => {
        setToAddUser(user);
    };

    const handleAddUserDialogClose = () => {
        setToAddUser(null);
    };

    const HandleAddMember = () => {
        if (workspace.workspace?.id && toAddUser?.id) {
            dispatch(addWorkspaceMember({
                workspaceId: workspace.workspace?.id,
                userId: toAddUser?.id,
                roleId: toAddUserRole
            }));

            handleAddUserDialogClose();
        }
    }

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
                        <>
                            <Breadcrumbs aria-label="breadcrumb" sx={{ mb: 2 }}>
                                <Link underline="hover" color="inherit" component={RouterLink} to={"/workspaces/" + workspace?.workspace?.id}>
                                    {workspace?.workspace?.name}
                                </Link>
                                <Link underline="hover" color="inherit" component={RouterLink} to={"/workspaces/" + workspace?.workspace?.id + "/members"}>
                                    Members
                                </Link>
                                <Typography color="text.primary">Add</Typography>
                            </Breadcrumbs>
                            <Container maxWidth='md' sx={{ display: 'flex', justifyContent: 'center' }}>
                                <ButtonGroup sx={{ width: '100%', maxWidth: 400 }}>
                                    <TextField
                                        size='small'
                                        sx={{ flexGrow: 1 }}
                                        value={search}
                                        onChange={(e) => setSearch(e.target.value)} />
                                    <Button variant='outlined' sx={{ flexGrow: 1 }} onClick={HandleSearchUsers}>Search</Button>
                                </ButtonGroup>
                            </Container>
                            <Container>
                                {users.users &&
                                    <>
                                        {users.users.map(u =>
                                            <UserElement
                                                user={u}
                                                onAddMember={() => handleAddUserDialogOpen(u)}
                                            />)
                                        }
                                    </>
                                }
                            </Container>
                        </>
                    }
                </>
            }
            <Dialog
                open={toAddUser !== null}
                onClose={handleAddUserDialogClose}
                transitionDuration={0}
            >
                <DialogContent>
                    <DialogContentText>
                        {`Add user ${toAddUser?.email} to the workspace?`}
                    </DialogContentText>
                    <FormControl fullWidth sx={{ mt: "10px" }}>
                        <InputLabel>Member Role</InputLabel>
                        <Select
                            value={toAddUserRole}
                            onChange={(e) => setToAddUserRole(e.target.value as number)}
                            size='small'
                            label="Member Role"
                        >
                            <MenuItem value={MEMBER_ROLE_ID}>{MEMBER_ROLE_NAME}</MenuItem>
                            <MenuItem value={VIEWER_ROLE_ID}>{VIEWER_ROLE_NAME}</MenuItem>
                        </Select>
                    </FormControl>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleAddUserDialogClose}>Cancel</Button>
                    <Button onClick={HandleAddMember} autoFocus>
                        Submit
                    </Button>
                </DialogActions>
            </Dialog>
        </Container >
    );
}