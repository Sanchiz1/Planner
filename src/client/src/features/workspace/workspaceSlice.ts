import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Workspace } from "../../Types/Workspace";
import { Membership } from "../../Types/Memership";
import { MembershipUser } from "../../Types/MembershipUser";

export interface WorkspaceType {
    workspace: Workspace | null,
    membership: Membership | null,
    members: MembershipUser[] | null,
    loading: boolean,
    success: boolean | null,
    error: string | null
}
const initialState: WorkspaceType =
{
    workspace: null,
    membership: null,
    members: null,
    loading: false,
    success: null,
    error: null
};

const workspaceSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        getWorkspace: (state, action: PayloadAction<number>) => {
            state.workspace = null;
            state.loading = true;
            state.success = true;
            state.error = null;
        },
        getWorkspaceSuccess: (state, action: PayloadAction<Workspace>) => {
            state.workspace = action.payload;
            state.loading = false;
            state.success = true;
            state.error = null;
        },
        getWorkspaceFailure: (state, action: PayloadAction<string>) => {
            state.workspace = null;
            state.loading = false;
            state.success = false;
            state.error = action.payload;
        },
        getWorkspaceMembership: (state, action: PayloadAction<number>) => {
            state.membership = null;
        },
        getWorkspaceMembershipSuccess: (state, action: PayloadAction<Membership>) => {
            state.membership = action.payload;
        },
        getWorkspaceMembershipFailure: (state, action: PayloadAction<Membership>) => {
            state.membership = null;
        }
    }
})

export const {
    getWorkspace,
    getWorkspaceSuccess,
    getWorkspaceFailure,
    getWorkspaceMembership,
    getWorkspaceMembershipSuccess,
    getWorkspaceMembershipFailure
} = workspaceSlice.actions;

export default workspaceSlice.reducer;