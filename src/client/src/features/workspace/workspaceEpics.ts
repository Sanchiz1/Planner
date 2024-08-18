import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { Membership } from "../../Types/Memership";
import { getworkspace, getWorkspaceFailure, getWorkspaceSuccess } from "./workspaceSlice";
import { GetWorkspace } from "../../API/WorkspaceRequests";
import { Workspace } from "../../Types/Workspace";

export const getWorkspaceEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(getworkspace.type),
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