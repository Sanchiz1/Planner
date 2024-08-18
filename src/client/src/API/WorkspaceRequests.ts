import { MembershipUser } from "../Types/MembershipUser";
import { Membership } from "../Types/Memership";
import { Workspace } from "../Types/Workspace";
import { GetAjaxObservable } from "./APIUtils";

export function GetMemberWorkspaces() {
    return GetAjaxObservable<Membership[]>(`/workspace/member`, "GET", true, { 'Content-Type': 'application/json' });
}

export function GetWorkspace(id: number) {
    return GetAjaxObservable<Workspace>(`/workspace/${id}`, "GET", false, { 'Content-Type': 'application/json' });
}

export function GetWorkspaceMembers(id: number) {
    return GetAjaxObservable<MembershipUser[]>(`/workspace/${id}/members`, "GET", false, { 'Content-Type': 'application/json' });
}

export function GetWorkspaceMembership(id: number) {
    return GetAjaxObservable<Membership>(`/workspace/${id}/membership`, "GET", true, { 'Content-Type': 'application/json' });
}
