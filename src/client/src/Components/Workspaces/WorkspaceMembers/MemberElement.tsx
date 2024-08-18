import { Card, CardContent, Grid, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { MembershipUser } from '../../../Types/MembershipUser';

interface MemershipWorkspaceElementProps {
    membershipUser: MembershipUser
}

export default function MemberElement(props: MemershipWorkspaceElementProps) {
    const { membershipUser } = props;
    const navigate = useNavigate();

    return (
        <Card sx={{
            mt: "5px",
            mr: "5px"
        }}>
            <CardContent>
                <Grid display='flex' flexDirection='row' alignItems='center'>
                    <Grid>
                        <Typography variant="h5" component="div">
                            {membershipUser.user.displayName}
                        </Typography>
                        <Typography variant="subtitle1" color='grey' component="div">
                            {membershipUser.user.email}
                        </Typography>
                    </Grid>
                    <Grid sx={{ml: 'auto'}}>
                        <Typography variant="subtitle1" color='grey' component="div">
                            {membershipUser.membership.role.name}
                        </Typography>
                    </Grid>
                </Grid>
            </CardContent>
        </Card>
    );
}