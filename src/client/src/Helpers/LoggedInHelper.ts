import { ACCESS_TOKEN_STORAGE_NAME } from "../config";

export function IsLoggedIn(): boolean {
    let token = localStorage.getItem(ACCESS_TOKEN_STORAGE_NAME);

    return token ? true : false
}