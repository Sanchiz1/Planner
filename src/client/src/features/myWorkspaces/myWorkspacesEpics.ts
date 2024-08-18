import { Action } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { getMyWorkspaces, getMyWorkspacesFailure, getMyWorkspacesSuccess } from "./myWorkspacesSlice";
import { GetMemerWorkspaces } from "../../API/WorkspaceRequests";
import { Membership } from "../../Types/Memership";

export const getMyWorkspacesEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(getMyWorkspaces.type),
    mergeMap(() =>
        GetMemerWorkspaces().pipe(
            map((res: Membership[]) => {
                return getMyWorkspacesSuccess(res);
            }
            )
        )),
    catchError((error, caught) =>
        merge(of(getMyWorkspacesFailure(error)),
            caught
        ))
);