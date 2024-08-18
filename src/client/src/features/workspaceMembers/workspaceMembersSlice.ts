import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Workspace } from "../../Types/Workspace";
import { Membership } from "../../Types/Memership";
import { MembershipUser } from "../../Types/MembershipUser";

export interface WorkspaceMembersType {
    members: MembershipUser[] | null,
    loading: boolean,
    success: boolean | null,
    error: string | null,
    updating: boolean,
    updateSuccess: boolean | null,
    updateError: string | null
}
const initialState: WorkspaceMembersType =
{
    members: null,
    loading: false,
    success: null,
    error: null,
    updating: false,
    updateSuccess: null,
    updateError: null
};

const workspaceSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        getWorkspaceMembers: (state, action: PayloadAction<number>) => {
            state.members = null;
            state.loading = true;
            state.success = true;
            state.error = null;
        },
        getWorkspaceMembersSuccess: (state, action: PayloadAction<MembershipUser[]>) => {
            state.members = action.payload;
            state.loading = false;
            state.success = true;
            state.error = null;
        },
        getWorkspaceMembersFailure: (state, action: PayloadAction<string>) => {
            state.members = null;
            state.loading = false;
            state.success = false;
            state.error = action.payload;
        },
        updateWorkspaceMember: (state, action: PayloadAction<MembershipUser>) => {
            state.updating = true;
            state.updateSuccess = null;
            state.updateError = null;
        },
        updateWorkspaceMemberSuccess: (state, action: PayloadAction<MembershipUser>) => {
            state.updating = false;
            state.updateSuccess = true;
            state.updateError = null;
            
            const index = state.members?.findIndex(member => member.membership.id === action.payload.membership.id);
            if (index !== undefined && state.members) {
                state.members[index] = action.payload;
            }
        },
        updateWorkspaceMemberFailure: (state, action: PayloadAction<string>) => {
            state.updating = false;
            state.updateSuccess = false;
            state.updateError = action.payload;
        }
    }
})

export const {
    getWorkspaceMembers,
    getWorkspaceMembersSuccess,
    getWorkspaceMembersFailure,
    updateWorkspaceMember,
    updateWorkspaceMemberSuccess,
    updateWorkspaceMemberFailure
} = workspaceSlice.actions;

export default workspaceSlice.reducer;