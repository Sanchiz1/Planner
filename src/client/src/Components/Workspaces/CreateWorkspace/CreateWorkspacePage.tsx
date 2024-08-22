import { Button, Grid, Switch, TextField, Typography } from '@mui/material';
import Container from '@mui/material/Container';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { createWorkspace, createWorkspaceWait, updateWorkspaceWait } from '../../../features/workspace/workspaceSlice';
import { ShowFailure, ShowSuccess } from '../../../Helpers/SnackBarHelper';
import { useAppDispatch, useAppSelector } from '../../../store';
import { Workspace } from '../../../Types/Workspace';

export default function CreateWorkspacePage() {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const [workspaceToCreate, setWorkspaceToCreate] = useState<Workspace>({ name: "New workspace", isPublic: true } as Workspace);
    const workspace = useAppSelector(state => state.workspace);

    const HandleWorkspaceNameChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setWorkspaceToCreate({ ...workspaceToCreate, name: e.target.value });
    }

    const HandleWorkspaceVisibilityChange = () => {
        setWorkspaceToCreate({ ...workspaceToCreate, isPublic: !workspaceToCreate.isPublic });
    }

    const HandleCreateWorkspace = () => {
        dispatch(createWorkspace(workspaceToCreate));
    }

    useEffect(() => {
        if (workspace.createSuccess) {
            ShowSuccess("Workspace created successfully");
            if (workspace.workspace?.id)
                navigate("/workspaces/" + workspace.workspace.id);
            dispatch(createWorkspaceWait());
        };

        if (workspace.createError) {
            ShowFailure(workspace.createError);
            dispatch(createWorkspaceWait());
        }
    }, [workspace.createSuccess, workspace.createError])

    return (
        <Container component="main" maxWidth='xl' sx={{
            pt: 6, pb: 6,
            bgcolor: 'background.default'
        }}>
            <Typography
                component="h1"
                variant="h2"
                align="left"
                color="text.primary"
                gutterBottom
            >
                Create workspace
            </Typography>
            <Grid container spacing={2} direction="column">

                {/* Workspace Name */}
                <Grid item>
                    <Typography variant="subtitle2">Workspace Name</Typography>
                    <Grid sx={{ ml: "5px" }}>
                        <TextField
                            size="small"
                            value={workspaceToCreate.name}
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
                                <Switch checked={workspaceToCreate.isPublic} onClick={HandleWorkspaceVisibilityChange} />
                            </Grid>
                        </Grid>
                        <Typography variant="caption" color='grey'>Public workspace is accessible to everyone, private workspace is accessible only to members</Typography>
                    </Grid>
                </Grid>

                {/* Update Button */}
                <Grid item>
                    <Button variant="contained" size="small" onClick={HandleCreateWorkspace}>Create</Button>
                </Grid>
            </Grid>
        </Container >
    );
}