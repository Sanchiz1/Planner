import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { GetWorkspaceMembers } from "../../API/WorkspaceRequests";
import { MembershipUser } from "../../Types/MembershipUser";
import { getWorkspaceMembers, getWorkspaceMembersFailure, getWorkspaceMembersSuccess, updateWorkspaceMember, updateWorkspaceMemberFailure, updateWorkspaceMemberSuccess } from "./workspaceMembersSlice";


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

// export const updateWorkspaceMemberEpic = (action$: Observable<Action>) => action$.pipe(
//     ofType(updateWorkspaceMember.type),
//     mergeMap((action: PayloadAction<MembershipUser>) =>
//         UpdateWorkspaceMemberAPI(action.payload).pipe(
//             map(() => {
//                 return updateWorkspaceMemberSuccess(action.payload);
//             }),
//             catchError((error, caught) =>
//                 merge(
//                     of(updateWorkspaceMemberFailure(error.message)),
//                     caught
//                 )
//             )
//         )
//     )
// );