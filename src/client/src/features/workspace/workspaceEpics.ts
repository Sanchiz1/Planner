import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { GetWorkspace, UpdateWorkspace } from "../../API/WorkspaceRequests";
import { Workspace } from "../../Types/Workspace";
import { getWorkspace, getWorkspaceFailure, getWorkspaceSuccess, updateWorkspace, updateWorkspaceFailure, updateWorkspaceSuccess } from "./workspaceSlice";
import { updateWorkspaceMemberSuccess } from "../workspaceMembers/workspaceMembersSlice";

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
export const updateWorkspaceEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(updateWorkspace.type),
    mergeMap((action: PayloadAction<Workspace>) =>
        UpdateWorkspace(action.payload.id, action.payload.name, action.payload.isPublic).pipe(
            map(() => updateWorkspaceSuccess()),
            map(() => getWorkspace(action.payload.id))
        )),
    catchError((error, caught) =>
        merge(of(updateWorkspaceFailure(error.message)),
            caught
        ))
);