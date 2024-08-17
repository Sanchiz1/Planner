import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { User } from "../../Types/User";
import { ACCESS_TOKEN_STORAGE_NAME } from "../../config";
import { jwtDecode } from "jwt-decode";
import { UserJWTDecoded } from "../../Types/UserJWTDecoded";
import { LoginPayload } from "./types/loginPayload";

export interface AuthType {
    error: string | null,
    loading: boolean,
    success: boolean | null
}
const initialState: AuthType =
{
    error: null,
    loading: false,
    success: null
};

const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        login: (state, action: PayloadAction<LoginPayload>) => {
            state.success = null;
            state.loading = true;
            state.error = null;
        },
        loginSuccess: (state) => {
            state.success = true;
            state.loading = false;
            state.error = null;
        },
        loginFailure: (state, action: PayloadAction<string>) => {
            state.success = false;
            state.loading = false;
            state.error = action.payload;
        },
        logout: (state) => {
            state.success = null;
            state.loading = false;
            state.error = null;
        },
    }
})

export const {
    login,
    loginSuccess,
    loginFailure,
    logout
} = authSlice.actions;

export default authSlice.reducer;