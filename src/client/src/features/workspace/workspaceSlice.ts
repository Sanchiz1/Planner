import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Workspace } from "../../Types/Workspace";

export interface WorkspaceType {
    workspace: Workspace | null,
    loading: boolean,
    success: boolean | null,
    error: string | null,
    updating: boolean,
    updateSuccess: boolean | null,
    updateError: string | null,
    deleting: boolean,
    deleteSuccess: boolean | null,
    deleteError: string | null,
    creating: boolean,
    createSuccess: boolean | null,
    createError: string | null
}
const initialState: WorkspaceType =
{
    workspace: null,
    loading: false,
    success: null,
    error: null,
    updating: false,
    updateSuccess: null,
    updateError: null,
    deleting: false,
    deleteSuccess: null,
    deleteError: null,
    creating: false,
    createSuccess: null,
    createError: null
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
            state.updateSuccess = null;
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
        },
        updateWorkspaceWait: (state) => {
            state.updating = false;
            state.updateSuccess = null;
            state.updateError = null;
        },
        deleteWorkspace: (state, action: PayloadAction<number>) => {
            state.deleting = true;
            state.deleteSuccess = null;
            state.deleteError = null;
        },
        deleteWorkspaceSuccess: (state) => {
            state.deleting = false;
            state.deleteSuccess = true;
            state.deleteError = null;
        },
        deleteWorkspaceFailure: (state, action: PayloadAction<string>) => {;
            state.deleting = false;
            state.deleteSuccess = false;
            state.deleteError = action.payload;
        },
        deleteWorkspaceWait: (state) => {
            state.deleting = false;
            state.deleteSuccess = null;
            state.deleteError = null;
        },
        createWorkspace: (state, action: PayloadAction<Workspace>) => {
            state.workspace = null;
            state.creating = true;
            state.createSuccess = null;
            state.createError = null;
        },
        createWorkspaceSuccess: (state) => {
            state.creating = false;
            state.createSuccess = true;
            state.createError = null;
        },
        createWorkspaceFailure: (state, action: PayloadAction<string>) => {;
            state.creating = false;
            state.createSuccess = false;
            state.createError = action.payload;
        },
        createWorkspaceWait: (state) => {
            state.creating = false;
            state.createSuccess = null;
            state.createError = null;
        }
    }
})

export const {
    getWorkspace,
    getWorkspaceSuccess,
    getWorkspaceFailure,
    updateWorkspace,
    updateWorkspaceSuccess,
    updateWorkspaceFailure,
    updateWorkspaceWait,
    deleteWorkspace,
    deleteWorkspaceSuccess,
    deleteWorkspaceFailure,
    deleteWorkspaceWait,
    createWorkspace,
    createWorkspaceSuccess,
    createWorkspaceFailure,
    createWorkspaceWait
} = workspaceSlice.actions;

export default workspaceSlice.reducer;