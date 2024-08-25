import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Workspace } from "../../Types/Workspace";
import { Membership } from "../../Types/Memership";
import { MembershipUser } from "../../Types/MembershipUser";

export interface WorkspaceType {
    membership: Membership | null,
    loading: boolean,
    success: boolean | null,
    error: string | null
}
const initialState: WorkspaceType =
{
    membership: null,
    loading: false,
    success: null,
    error: null
};

const workspaceSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        getWorkspaceMembership: (state, action: PayloadAction<number>) => {
            state.membership = null;
            state.loading = true;
            state.success = true;
            state.error = null;
        },
        getWorkspaceMembershipSuccess: (state, action: PayloadAction<Membership>) => {
            state.membership = action.payload;
            state.loading = false;
            state.success = true;
            state.error = null;
        },
        getWorkspaceMembershipFailure: (state, action: PayloadAction<string>) => {
            state.membership = null;
            state.loading = false;
            state.success = false;
            state.error = action.payload;
        }
    }
})

export const {
    getWorkspaceMembership,
    getWorkspaceMembershipSuccess,
    getWorkspaceMembershipFailure
} = workspaceSlice.actions;

export default workspaceSlice.reducer;