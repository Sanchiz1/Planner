import Button from '@mui/material/Button';
import { Card, CardActionArea, CardActions, CardContent, Paper, Typography } from '@mui/material';
import { Workspace } from '../../Types/Workspace';

interface WorkspaceElementProps {
    workspace: Workspace
}

export default function WorkspaceElement(props: WorkspaceElementProps) {

    return (
        <Card sx={{
            mt: "5px",
            mr: "10px"
        }}>
            <CardActionArea>
                <CardContent>
                    <Typography variant="h5" component="div">
                        {props.workspace.name}
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
}