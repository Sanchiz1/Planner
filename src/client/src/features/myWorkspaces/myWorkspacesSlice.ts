import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { User } from "../../Types/User";
import { ACCESS_TOKEN_STORAGE_NAME } from "../../config";
import { jwtDecode } from "jwt-decode";
import { UserJWTDecoded } from "../../Types/UserJWTDecoded";
import { Membership } from "../../Types/Memership";

export interface MyWorkspacesType {
    memerships: Membership[] | null,
    loading: boolean,
    success: boolean | null,
    error: string | null
}
const initialState: MyWorkspacesType =
{
    memerships: null,
    loading: false,
    success: null,
    error: null
};

const myWorkspacesSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        getMyWorkspaces: (state) => {
            state.memerships = null;
            state.loading = true;
            state.success = true;
            state.error = null;
        },
        getMyWorkspacesSuccess: (state, action: PayloadAction<Membership[]>) => {
            state.memerships = action.payload;
            state.loading = false;
            state.success = true;
            state.error = null;
        },
        getMyWorkspacesFailure: (state, action: PayloadAction<string>) => {
            state.memerships = null;
            state.loading = false;
            state.success = false;
            state.error = action.payload;
        }
    }
})

export const {
    getMyWorkspaces,
    getMyWorkspacesSuccess,
    getMyWorkspacesFailure
} = myWorkspacesSlice.actions;

export default myWorkspacesSlice.reducer;