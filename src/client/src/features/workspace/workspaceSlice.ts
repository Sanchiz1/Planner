import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Workspace } from "../../Types/Workspace";

export interface WorkspaceType {
    workspace: Workspace | null,
    loading: boolean,
    success: boolean | null,
    error: string | null,
    updating: boolean,
    updateSuccess: boolean | null,
    updateError: string | null
}
const initialState: WorkspaceType =
{
    workspace: null,
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
        getWorkspace: (state, action: PayloadAction<number>) => {
            state.workspace = null;
            state.loading = true;
            state.success = null;
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
        updateWorkspace: (state, action: PayloadAction<Workspace>) => {
            state.updating = true;
            state.updateSuccess = false;
            state.updateError = null;
        },
        updateWorkspaceSuccess: (state) => {
            state.loading = false;
            state.updateSuccess = true;
            state.updateError = null;
        },
        updateWorkspaceFailure: (state, action: PayloadAction<string>) => {;
            state.loading = false;
            state.updateSuccess = false;
            state.updateError = action.payload;
        }
    }
})

export const {
    getWorkspace,
    getWorkspaceSuccess,
    getWorkspaceFailure,
    updateWorkspace,
    updateWorkspaceSuccess,
    updateWorkspaceFailure
} = workspaceSlice.actions;

export default workspaceSlice.reducer;