import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { GetWorkspaceMembers, RemoveWorkspaceMember, UpdateWorkspaceMemberRole } from "../../API/WorkspaceRequests";
import { MembershipUser } from "../../Types/MembershipUser";
import { getWorkspaceMembers, getWorkspaceMembersFailure, getWorkspaceMembersSuccess, removeWorkspaceMember, removeWorkspaceMemberFailure, removeWorkspaceMemberSuccess, updateWorkspaceMember, updateWorkspaceMemberFailure, updateWorkspaceMemberSuccess } from "./workspaceMembersSlice";
import { RemoveMemberPayload } from "./types/RemoveMemberPayload";
import { UpdateWorkspaceMemberPayload } from "./types/UpdateWorkspaceMemberPayload";


export const getWorkspaceMembersEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(getWorkspaceMembers.type),
    mergeMap((action: PayloadAction<number>) =>
        GetWorkspaceMembers(action.payload).pipe(
            map((res: MembershipUser[]) => {
                return getWorkspaceMembersSuccess(res);
            }
            )
        )),
    catchError((error, caught) =>
        merge(of(getWorkspaceMembersFailure(error.message)),
            caught
        ))
);

export const removeWorkspaceMemberEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(removeWorkspaceMember.type),
    mergeMap((action: PayloadAction<RemoveMemberPayload>) =>
        RemoveWorkspaceMember(action.payload.workspaceId, action.payload.ToRemoveMembershipId).pipe(
            map(() => removeWorkspaceMemberSuccess(action.payload)),
        )
    ),
    catchError((error, caught) =>
        merge(of(removeWorkspaceMemberFailure(error.message)),
            caught
        ))
);

export const updateWorkspaceMemberEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(updateWorkspaceMember.type),
    mergeMap((action: PayloadAction<UpdateWorkspaceMemberPayload>) =>
        UpdateWorkspaceMemberRole(action.payload.workspaceId, action.payload.ToUpdateMembershipId, action.payload.ToUpdateRoleId).pipe(
            map(() => {
                return updateWorkspaceMemberSuccess(action.payload);
            })
        )
    ),
    catchError((error, caught) =>
        merge(
            of(updateWorkspaceMemberFailure(error.message)),
            caught
        )
    )
);