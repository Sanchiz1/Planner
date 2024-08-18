import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Workspace } from "../../Types/Workspace";

export interface WorkspaceType {
    workspace: Workspace | null,
    loading: boolean,
    success: boolean | null,
    error: string | null
}
const initialState: WorkspaceType =
{
    workspace: null,
    loading: false,
    success: null,
    error: null
};

const workspaceSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        getworkspace: (state, action: PayloadAction<number>) => {
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
        }
    }
})

export const {
    getworkspace,
    getWorkspaceSuccess,
    getWorkspaceFailure
} = workspaceSlice.actions;

export default workspaceSlice.reducer;