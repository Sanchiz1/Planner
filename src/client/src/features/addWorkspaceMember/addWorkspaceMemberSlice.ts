import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Workspace } from "../../Types/Workspace";
import { Membership } from "../../Types/Memership";
import { MembershipUser } from "../../Types/MembershipUser";
import { User } from "../../Types/User";

export interface AddWorkspaceMemberType {
    users: User[] | null,
    loading: boolean,
    success: boolean | null,
    error: string | null,
    adding: boolean,
    addingSuccess: boolean | null,
    addingError: string | null
}
const initialState: AddWorkspaceMemberType =
{
    users: null,
    loading: false,
    success: null,
    error: null,
    adding: false,
    addingSuccess: null,
    addingError: null
};

const workspaceSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        getUsers: (state, action: PayloadAction<string>) => {
            state.users = null;
            state.loading = true;
            state.success = true;
            state.error = null;
        },
        getUsersSuccess: (state, action: PayloadAction<User[]>) => {
            state.users = action.payload;
            state.loading = false;
            state.success = true;
            state.error = null;
        },
        getUsersFailure: (state, action: PayloadAction<string>) => {
            state.users = null;
            state.loading = false;
            state.success = false;
            state.error = action.payload;
        },
        addWorkspaceMember: (state, action: PayloadAction<number>) => {
            state.adding = true;
            state.addingSuccess = null;
            state.addingError = null;
        },
        addWorkspaceMemberSuccess: (state) => {
            state.adding = false;
            state.addingSuccess = true;
            state.addingError = null;
        },
        addWorkspaceMemberFailure: (state, action: PayloadAction<string>) => {
            state.adding = false;
            state.addingSuccess = false;
            state.addingError = action.payload;
        }
    }
})

export const {
    getUsers,
    getUsersSuccess,
    getUsersFailure,
    addWorkspaceMember,
    addWorkspaceMemberSuccess,
    addWorkspaceMemberFailure
} = workspaceSlice.actions;

export default workspaceSlice.reducer;