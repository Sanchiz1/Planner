import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { GetWorkspace } from "../../API/WorkspaceRequests";
import { Workspace } from "../../Types/Workspace";
import { getWorkspace, getWorkspaceFailure, getWorkspaceSuccess } from "./workspaceSlice";

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