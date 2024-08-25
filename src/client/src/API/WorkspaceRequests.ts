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

export function AddWorkspaceMember(workspaceId: number, userId: number, roleId: number) {
    return GetAjaxObservable(`/workspace/${workspaceId}/members`, "POST", true, { 'Content-Type': 'application/json' }, false,
        {
            "toAddUserId": userId,
            "toAddRoleId": roleId
        });
}

export function RemoveWorkspaceMember(workspaceId: number, toRemoveMembershipId: number) {
    return GetAjaxObservable(`/workspace/${workspaceId}/members`, "DELETE", true, { 'Content-Type': 'application/json' }, false,
        {
            "toRemoveMembershipId": toRemoveMembershipId
        });
}

export function UpdateWorkspaceMemberRole(workspaceId: number, toUpdateMembershipId: number, toUpdateRoleId: number) {
    return GetAjaxObservable(`/workspace/${workspaceId}/members`, "PATCH", true, { 'Content-Type': 'application/json' }, false,
        {
            "toUpdateMembershipId": toUpdateMembershipId,
            "toUpdateRoleId": toUpdateRoleId
        });
}


export function GetWorkspaceMembership(id: number) {
    return GetAjaxObservable<Membership>(`/workspace/${id}/membership`, "GET", true, { 'Content-Type': 'application/json' });
}

export function UpdateWorkspace(workspaceId: number, workspaceName: string, workspaceIsPublic: boolean) {
    return GetAjaxObservable(`/workspace/${workspaceId}`, "PUT", true, { 'Content-Type': 'application/json' }, false,
        {
            "workspaceName": workspaceName,
            "workspaceIsPublic": workspaceIsPublic
        }
    );
}

export function DeleteWorkspace(workspaceId: number) {
    return GetAjaxObservable(`/workspace/${workspaceId}`, "DELETE", true, { 'Content-Type': 'application/json' });
}

export function CreateWorkspace(workspaceName: string, workspaceIsPublic: boolean) {
    return GetAjaxObservable<number>(`/workspace`, "POST", true, { 'Content-Type': 'application/json' }, false,
        {
            "workspaceName": workspaceName,
            "workspaceIsPublic": workspaceIsPublic
        });
}