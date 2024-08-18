import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { GetWorkspace, GetWorkspaceMembership } from "../../API/WorkspaceRequests";
import { Membership } from "../../Types/Memership";
import { Workspace } from "../../Types/Workspace";
import { getWorkspace, getWorkspaceFailure, getWorkspaceMembership, getWorkspaceMembershipFailure, getWorkspaceMembershipSuccess, getWorkspaceSuccess } from "./workspaceSlice";

export const getWorkspaceEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(getWorkspace.type),
    mergeMap((action: PayloadAction<number>) =>
        GetWorkspace(action.payload).pipe(
            map((res: Workspace) => {
                return getWorkspaceSuccess(res);
            }
            )
        )),
    catchError((error, caught) =>
        merge(of(getWorkspaceFailure(error.message)),
            caught
        ))
);

export const getWorkspaceMembershipEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(getWorkspaceMembership.type),
    mergeMap((action: PayloadAction<number>) =>
        GetWorkspaceMembership(action.payload).pipe(
            map((res: Membership) => {
                return getWorkspaceMembershipSuccess(res);
            }
            )
        )),
    catchError((error, caught) =>
        merge(of(getWorkspaceMembershipFailure(error.message)),
            caught
        ))
);