import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Workspace } from "../../Types/Workspace";
import { Membership } from "../../Types/Memership";
import { MembershipUser } from "../../Types/MembershipUser";
import { User } from "../../Types/User";
import { AddMemberPayload } from "./types/addMemberPayload";

export interface AddWorkspaceMemberType {
    users: User[] | null,
    loading: boolean,
    success: boolean | null,
    error: string | null,
    adding: boolean,
    addSuccess: boolean | null,
    addError: string | null
}
const initialState: AddWorkspaceMemberType =
{
    users: null,
    loading: false,
    success: null,
    error: null,
    adding: false,
    addSuccess: null,
    addError: null
};

const usersSlice = createSlice({
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
        addWorkspaceMember: (state, action: PayloadAction<AddMemberPayload>) => {
            state.adding = true;
            state.addSuccess = null;
            state.addError = null;
        },
        addWorkspaceMemberSuccess: (state) => {
            state.adding = false;
            state.addSuccess = true;
            state.addError = null;
        },
        addWorkspaceMemberFailure: (state, action: PayloadAction<string>) => {
            state.adding = false;
            state.addSuccess = false;
            state.addError = action.payload;
        },
        addWorkspaceMemberWait: (state) => {
            state.adding = false;
            state.addSuccess = null;
            state.addError = null;
        }
    }
})

export const {
    getUsers,
    getUsersSuccess,
    getUsersFailure,
    addWorkspaceMember,
    addWorkspaceMemberSuccess,
    addWorkspaceMemberFailure,
    addWorkspaceMemberWait
} = usersSlice.actions;

export default usersSlice.reducer;