import { Breadcrumbs, Button, Grid, Link, Switch, TextField, Typography } from '@mui/material';
import Box from '@mui/material/Box';
import { useState } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import { useAppDispatch } from '../../../store';
import { Membership } from '../../../Types/Memership';
import { Workspace } from '../../../Types/Workspace';

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
                        <Typography variant="subtitle1" color='grey'>Workspace Name</Typography>
                        <TextField
                            size="small"
                            value={workspaceToEdit.name}
                            onChange={(e) => HandleWorkspaceNameChange(e)}
                            variant="outlined"
                        />
                    </Grid>

                    {/* Workspace Visibility */}
                    <Grid item>
                        <Typography variant="subtitle1" color='grey'>Workspace Visibility</Typography>
                        <Grid container alignItems="center">
                            <Grid item>
                                <Typography>Public</Typography>
                            </Grid>
                            <Grid item>
                                <Switch checked={workspaceToEdit.isPublic} onClick={HandleWorkspaceVisibilityChange} />
                            </Grid>
                        </Grid>
                    </Grid>

                    {/* Update Button */}
                    <Grid item>
                        <Button variant="contained" size="small" onClick={HandleEditWorkspace}>Save</Button>
                    </Grid>
                </Grid>
            </Box>
        </>
    );
}