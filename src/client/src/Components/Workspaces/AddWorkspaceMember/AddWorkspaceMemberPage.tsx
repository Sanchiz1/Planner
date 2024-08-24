import { Breadcrumbs, Button, ButtonGroup, Link, TextField, Typography } from '@mui/material';
import Container from '@mui/material/Container';
import { useEffect, useState } from 'react';
import { Link as RouterLink, useParams } from 'react-router-dom';
import { OWNER_ROLE_NAME } from '../../../config';
import { getWorkspace } from '../../../features/workspace/workspaceSlice';
import { getWorkspaceMembers } from '../../../features/workspaceMembers/workspaceMembersSlice';
import { getWorkspaceMembership } from '../../../features/workspaceMembership/workspaceMembershipSlice';
import { useAppDispatch, useAppSelector } from '../../../store';
import { getUsers } from '../../../features/addWorkspaceMember/addWorkspaceMemberSlice';
import UserElement from './UserElement';

export default function AddWorkspaceMemberPage() {
    const dispatch = useAppDispatch();
    const { workspaceId } = useParams();
    const workspace = useAppSelector(state => state.workspace);
    const workspaceMembership = useAppSelector(state => state.workspaceMembership);
    const addWorkspaceMember = useAppSelector(state => state.addWorkspaceMember);

    const [search, setSearch] = useState<string>("");

    useEffect(() => {
        if (!workspaceId) return;

        const id = parseInt(workspaceId);

        dispatch(getWorkspace(id));
        dispatch(getWorkspaceMembership(id));
    }, [workspaceId]);

    const HandleSearchUsers = () => {
        if (search.length === 0) return;

        dispatch(getUsers(search));
    }

    const HandleAddMember = (userId: number) => {
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
                                {addWorkspaceMember.users &&
                                    <>
                                        {addWorkspaceMember.users.map(u =>
                                            <UserElement
                                                user={u}
                                                onAddMember={() => HandleAddMember(u.id)}
                                            />)
                                        }
                                    </>
                                }
                            </Container>
                        </>
                    }
                </>
            }
        </Container >
    );
}