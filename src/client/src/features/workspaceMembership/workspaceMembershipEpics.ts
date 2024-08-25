import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { GetWorkspaceMembership } from "../../API/WorkspaceRequests";
import { Membership } from "../../Types/Memership";
import { getWorkspaceMembership, getWorkspaceMembershipFailure, getWorkspaceMembershipSuccess } from "./workspaceMembershipSlice";

export const getWorkspaceMembershipEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(getWorkspaceMembership.type),
    mergeMap((action: PayloadAction<number>) =>
        GetWorkspaceMembership(action.payload).pipe(
            map((res: Membership) => {
                return getWorkspaceMembershipSuccess(res);
            })
        )),
    catchError((error, caught) =>
        merge(of(getWorkspaceMembershipFailure(error.message)),
            caught
        ))
);