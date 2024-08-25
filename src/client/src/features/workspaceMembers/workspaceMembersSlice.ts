import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Workspace } from "../../Types/Workspace";
import { Membership } from "../../Types/Memership";
import { MembershipUser } from "../../Types/MembershipUser";
import { RemoveMemberPayload } from "./types/RemoveMemberPayload";
import { UpdateWorkspaceMemberPayload } from "./types/UpdateWorkspaceMemberPayload";

export interface WorkspaceMembersType {
    members: MembershipUser[] | null,
    loading: boolean,
    success: boolean | null,
    error: string | null,
    updating: boolean,
    updateSuccess: boolean | null,
    updateError: string | null,
    removing: boolean,
    removeSuccess: boolean | null,
    removeError: string | null
}
const initialState: WorkspaceMembersType =
{
    members: null,
    loading: false,
    success: null,
    error: null,
    updating: false,
    updateSuccess: null,
    updateError: null,
    removing: false,
    removeSuccess: null,
    removeError: null
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
        updateWorkspaceMember: (state, action: PayloadAction<UpdateWorkspaceMemberPayload>) => {
            state.updating = true;
            state.updateSuccess = null;
            state.updateError = null;
        },
        updateWorkspaceMemberSuccess: (state, action: PayloadAction<UpdateWorkspaceMemberPayload>) => {
            state.updating = false;
            state.updateSuccess = true;
            state.updateError = null;

            const index = state.members?.findIndex(member => member.membership.id === action.payload.ToUpdateMembershipId);
            if (index !== undefined && state.members) {
                state.members[index].membership.roleId = action.payload.ToUpdateRoleId;
            };
        },
        updateWorkspaceMemberFailure: (state, action: PayloadAction<string>) => {
            state.updating = false;
            state.updateSuccess = false;
            state.updateError = action.payload;
        },
        updateWorkspaceMemberWait: (state) => {
            state.updating = false;
            state.updateSuccess = null;
            state.updateError = null;
        },
        removeWorkspaceMember: (state, action: PayloadAction<RemoveMemberPayload>) => {
            state.removing = true;
            state.removeSuccess = null;
            state.removeError = null;
        },
        removeWorkspaceMemberSuccess: (state, action: PayloadAction<RemoveMemberPayload>) => {
            state.removing = false;
            state.removeSuccess = true;
            state.removeError = null;

            if (state.members) {
                state.members = state.members.filter(member => member.membership.id !== action.payload.ToRemoveMembershipId);
            }
        },
        removeWorkspaceMemberFailure: (state, action: PayloadAction<string>) => {
            state.removing = false;
            state.removeSuccess = false;
            state.removeError = action.payload;
        },
        removeWorkspaceMemberWait: (state) => {
            state.removing = false;
            state.removeSuccess = null;
            state.removeError = null;
        },
    }
})

export const {
    getWorkspaceMembers,
    getWorkspaceMembersSuccess,
    getWorkspaceMembersFailure,
    updateWorkspaceMember,
    updateWorkspaceMemberSuccess,
    updateWorkspaceMemberFailure,
    updateWorkspaceMemberWait,
    removeWorkspaceMember,
    removeWorkspaceMemberFailure,
    removeWorkspaceMemberSuccess,
    removeWorkspaceMemberWait
} = workspaceSlice.actions;

export default workspaceSlice.reducer;