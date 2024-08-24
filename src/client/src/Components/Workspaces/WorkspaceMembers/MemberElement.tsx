import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import { Card, CardContent, Grid, IconButton, MenuItem, Select, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { MEMBER_ROLE_ID, MEMBER_ROLE_NAME, OWNER_ROLE_ID, OWNER_ROLE_NAME, VIEWER_ROLE_ID, VIEWER_ROLE_NAME } from '../../../config';
import { MembershipUser } from '../../../Types/MembershipUser';
import { Membership } from '../../../Types/Memership';

interface MemershipWorkspaceElementProps {
    membershipUser: MembershipUser,
    membership: Membership | null,
    onUpdateMember: (roleId: number) => void,
    onRemoveMember: () => void,
}

export default function MemberElement(props: MemershipWorkspaceElementProps) {
    const { membershipUser, membership, onUpdateMember, onRemoveMember } = props;

    const HandleUpdateMember = (roleId: number) => {
        onUpdateMember(roleId);
    }

    const HandleRemoveMember = () => {
        onRemoveMember();
    }

    return (
        <Card sx={{
            mt: "5px",
            mr: "5px"
        }}>
            <CardContent>
                <Grid display='flex' flexDirection='row' alignItems='center'>
                    <Grid sx={{ mr: 'auto' }}>
                        <Typography variant="h5" component="div">
                            {membershipUser.user.displayName}
                        </Typography>
                        <Typography variant="subtitle1" color='grey' component="div">
                            {membershipUser.user.email}
                        </Typography>
                    </Grid>
                    {membership?.role.name === OWNER_ROLE_NAME ?
                        <>
                            <Grid>
                                <IconButton sx={{ color: 'grey', borderRadius: '7px' }}><DeleteOutlineIcon /></IconButton>
                            </Grid>
                            <Grid sx={{ ml: '5px' }}>
                                <Select
                                    labelId="demo-customized-select-label"
                                    id="demo-customized-select"
                                    value={membershipUser.membership.role.id}
                                    onChange={(e) => HandleUpdateMember(e.target.value as number)}
                                    size='small'
                                    sx={{ color: 'gray' }}
                                >
                                    <MenuItem value={OWNER_ROLE_ID}>{OWNER_ROLE_NAME}</MenuItem>
                                    <MenuItem value={MEMBER_ROLE_ID}>{MEMBER_ROLE_NAME}</MenuItem>
                                    <MenuItem value={VIEWER_ROLE_ID}>{VIEWER_ROLE_NAME}</MenuItem>
                                </Select>
                            </Grid>
                        </>
                        :
                        <Grid sx={{ ml: '5px' }}>
                            <Typography variant="subtitle1" color='grey' component="div">
                                {membershipUser.membership.role.name}
                            </Typography>
                        </Grid>
                    }
                </Grid>
            </CardContent>
        </Card>
    );
}