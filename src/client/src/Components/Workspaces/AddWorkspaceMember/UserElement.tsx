import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import { Button, Card, CardContent, Grid, IconButton, Typography } from '@mui/material';
import { User } from '../../../Types/User';

interface UserElementProps {
    user: User,
    onAddMember: () => void
}

export default function UserElement(props: UserElementProps) {
    const { user, onAddMember } = props;

    const HandleAddMember = () => {
        onAddMember();
    }

    return (
        <Card sx={{
            mt: "5px",
            mr: "5px"
        }}>
            <CardContent sx={{ pt: "5px", '&:last-child': { pb: "5px" } }}>
                <Grid display='flex' flexDirection='row' alignItems='center'>
                    <Grid sx={{ mr: 'auto' }}>
                        <Typography variant="h5" component="div">
                            {user.displayName}
                        </Typography>
                        <Typography variant="subtitle1" color='grey' component="div">
                            {user.email}
                        </Typography>
                    </Grid>
                    <Grid>
                        <Button variant='contained' onClick={HandleAddMember}>Add</Button>
                    </Grid>
                </Grid>
            </CardContent>
        </Card>
    );
}