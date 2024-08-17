import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { User } from "../../Types/User";
import { ACCESS_TOKEN_STORAGE_NAME } from "../../config";
import { jwtDecode } from "jwt-decode";
import { UserJWTDecoded } from "../../Types/UserJWTDecoded";

export interface AccountType {
    user: User | null,
    loading: boolean,
    success: boolean | null,
    error: string | null
}
const initialState: AccountType =
{
    user: null,
    loading: false,
    success: null,
    error: null
};

const accountSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        getUser: (state) => {
            state.user = null;
            state.loading = true;
            state.success = true;
            state.error = null;
        },
        getUserSuccess: (state, action: PayloadAction<string>) => {
            const decoded = jwtDecode<UserJWTDecoded>(action.payload);

            state.user = { ...state.user, id: decoded.Id, email: decoded.Email, displayName: decoded.DisplayName };
            state.loading = false;
            state.success = true;
            state.error = null;
        },
        getUserFailure: (state, action: PayloadAction<string>) => {
            state.user = null;
            state.loading = false;
            state.success = false;
            state.error = action.payload;
        }
    }
})

export const {
    getUser,
    getUserSuccess,
    getUserFailure
} = accountSlice.actions;

export default accountSlice.reducer;