import { User } from "../Types/User";
import { GetAjaxObservable } from "./APIUtils";

export function GetUsers(email: string) {
    return GetAjaxObservable<User[]>(`/user?email=${email}`, "GET", false, { 'Content-Type': 'application/json' });
}