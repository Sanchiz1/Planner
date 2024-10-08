import { Breadcrumbs, Button, Divider, Grid, Link, Switch, TextField, Typography } from '@mui/material';
import Box from '@mui/material/Box';
import { useEffect, useState } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import { useAppDispatch } from '../../../store';
import { Membership } from '../../../Types/Memership';
import { Workspace } from '../../../Types/Workspace';
import { deleteWorkspace, updateWorkspace } from '../../../features/workspace/workspaceSlice';

type WorkspaceProps = {
    workspace: Workspace,
    membership: Membership | null
}

export default function WorkspaceSettingsComponent(props: WorkspaceProps) {
    const { workspace, membership } = props
    const [workspaceToEdit, setWorkspaceToEdit] = useState<Workspace>(workspace);
    const dispatch = useAppDispatch();

    const HandleWorkspaceNameChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setWorkspaceToEdit({ ...workspaceToEdit, name: e.target.value });
    }

    const HandleWorkspaceVisibilityChange = () => {
        setWorkspaceToEdit({ ...workspaceToEdit, isPublic: !workspaceToEdit.isPublic });
    }

    const HandleEditWorkspace = () => {
        dispatch(updateWorkspace(workspaceToEdit));
    }

    const HandleDeleteWorkspace = () => {
        dispatch(deleteWorkspace(workspace.id));
    }

    return (
        <>
            <Breadcrumbs aria-label="breadcrumb" sx={{ mb: 3 }}>
                <Link underline="hover" color="inherit" component={RouterLink} to={"/workspaces/" + workspace.id}>
                    {workspace.name}
                </Link>
                <Typography color="text.primary">Settings</Typography>
            </Breadcrumbs>
            <Box >
                <Grid container spacing={2} direction="column">

                    {/* Workspace Name */}
                    <Grid item>
                        <Typography variant="subtitle2">Workspace Name</Typography>
                        <Grid sx={{ ml: "5px" }}>
                            <TextField
                                size="small"
                                value={workspaceToEdit.name}
                                onChange={(e) => HandleWorkspaceNameChange(e)}
                                variant="outlined"
                            />
                        </Grid>
                    </Grid>

                    {/* Workspace Visibility */}
                    <Grid item>
                        <Typography variant="subtitle2">Workspace Visibility</Typography>
                        <Grid sx={{ ml: "5px" }}>
                            <Grid container alignItems="center">
                                <Grid item>
                                    <Typography>Public</Typography>
                                </Grid>
                                <Grid item>
                                    <Switch checked={workspaceToEdit.isPublic} onClick={HandleWorkspaceVisibilityChange} />
                                </Grid>
                            </Grid>
                            <Typography variant="caption" color='grey'>Public workspace is accessible to everyone, private workspace is accessible only to members</Typography>
                        </Grid>
                    </Grid>

                    {/* Update Button */}
                    <Grid item>
                        <Button variant="contained" size="small" onClick={HandleEditWorkspace}>Save</Button>
                    </Grid>
                    <Divider sx={{ my: "10px" }} />

                    <Grid item>
                        <Typography variant="subtitle2">Delete workspace</Typography>
                        <Typography variant="caption" color='grey'>Once you delete a repository, there is no going back. Please be certain.</Typography>
                        <Grid item sx={{ mt: "5px" }}>
                            <Button variant="outlined" size="small" color='error' onClick={HandleDeleteWorkspace}>Delete Workspace</Button>
                        </Grid>
                    </Grid>
                </Grid>
            </Box>
        </>
    );
}