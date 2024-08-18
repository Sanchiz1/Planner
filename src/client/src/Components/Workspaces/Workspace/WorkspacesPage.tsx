import { Typography } from '@mui/material';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getworkspace } from '../../../features/workspace/workspaceSlice';
import { ShowFailure } from '../../../Helpers/SnackBarHelper';
import { useAppDispatch, useAppSelector } from '../../../store';

export default function WorkspacePage() {
  const dispatch = useAppDispatch();
  const { workspaceId } = useParams();
  const { error, success, workspace } = useAppSelector(state => state.workspace);

  useEffect(() => {
    if (!workspaceId) return;
    dispatch(getworkspace(parseInt(workspaceId)));
  }, [workspaceId]);

  return (
    <Container component="main" maxWidth='xl' sx={{
      pt: 8, pb: 6,
      bgcolor: 'background.default'
    }}>
      {error && !workspace ?
        <Typography
          component="h1"
          variant="h2"
          align="left"
          color="text.primary"
          gutterBottom
        >
          {error.toString()}
        </Typography> :
        <Typography
          component="h1"
          variant="h2"
          align="left"
          color="text.primary"
          gutterBottom
        >
          {workspace?.name}
        </Typography>
      }
      <Box sx={{
        mt: "10px",
        display: "flex",
        flexWrap: "wrap"
      }}>
      </Box>
    </Container>
  );
}