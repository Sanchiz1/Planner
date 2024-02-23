import { getCookie } from "../Helpers/CookieHelper";

export function IsLoggedIn(): boolean {
    let token = getCookie("accessToken");

    return token ? true : false
}