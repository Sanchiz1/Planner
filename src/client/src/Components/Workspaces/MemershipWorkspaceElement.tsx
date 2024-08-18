import { Card, CardActionArea, CardContent, Typography } from '@mui/material';
import { Membership } from '../../Types/Memership';
import { useNavigate } from 'react-router-dom';

interface MemershipWorkspaceElementProps {
    membership: Membership
}

export default function MemershipWorkspaceElement(props: MemershipWorkspaceElementProps) {
    const navigate = useNavigate();

    const HandleElementClick = () => {
        navigate("workspaces/" + props.membership.workspaceId);
    }

    return (
        <Card sx={{
            mt: "5px",
            mr: "10px"
        }}>
            <CardActionArea onClick={HandleElementClick}>
                <CardContent>
                    <Typography variant="h5" component="div">
                        {props.membership.workspace.name}
                    </Typography>
                    <Typography variant="subtitle1" color='grey' component="div">
                        {props.membership.role.name}
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
}