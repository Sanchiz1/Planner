import { Membership } from "../Types/Memership";
import { Workspace } from "../Types/Workspace";
import { GetAjaxObservable } from "./APIUtils";

export function GetMemberWorkspaces() {
    return GetAjaxObservable<Membership[]>(`/workspace/member`, "GET", false, { 'Content-Type': 'application/json' });
}

export function GetWorkspace(id: number) {
    return GetAjaxObservable<Workspace>(`/workspace/${id}`, "GET", false, { 'Content-Type': 'application/json' });
}
