import { Button, Typography } from '@mui/material';
import Box from '@mui/material/Box';
import { useNavigate } from 'react-router-dom';
import { useAppDispatch } from '../../../store';
import { Membership } from '../../../Types/Memership';
import { Workspace } from '../../../Types/Workspace';

type WorkspaceProps = {
    workspace: Workspace,
    membership: Membership | null
}


export default function WorkspaceComponent(props: WorkspaceProps) {
    const { workspace, membership } = props;
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    const HandleMembersClick = () => {
        navigate("members");
    }

    const HandleSettingsClick = () => {
        navigate("settings");
    }

    return (
        <>
            <Typography
                component="h1"
                variant="h2"
                align="left"
                color="text.primary"
                gutterBottom
            >
                {workspace.name}
            </Typography>
            <Box>
                {membership?.role.name === "Owner" &&
                    <Button variant='contained' sx={{ mr: "5px" }} onClick={HandleSettingsClick}>Settings</Button>
                }
                <Button variant='contained' onClick={HandleMembersClick}>Members</Button>
            </Box>
        </>
    );
}