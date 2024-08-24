import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { GetWorkspaceMembers } from "../../API/WorkspaceRequests";
import { MembershipUser } from "../../Types/MembershipUser";
import { addWorkspaceMember, addWorkspaceMemberSuccess, getUsers, getUsersFailure, getUsersSuccess } from "./addWorkspaceMemberSlice";
import { User } from "../../Types/User";
import { GetUsers } from "../../API/UserRequests";

export const getUsersEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(getUsers.type),
    mergeMap((action: PayloadAction<string>) =>
        GetUsers(action.payload).pipe(
            map((res: User[]) => {
                return getUsersSuccess(res);
            }
            )
        )),
    catchError((error, caught) =>
        merge(of(getUsersFailure(error.message)),
            caught
        ))
);

// export const addWorkspaceMemberEpic = (action$: Observable<Action>) => action$.pipe(
//     ofType(addWorkspaceMember.type),
//     mergeMap((action: PayloadAction<number>) =>
//         GetUsers(action.payload).pipe(
//             map((res: User[]) => {
//                 return addWorkspaceMemberSuccess();
//             }
//             )
//         )),
//     catchError((error, caught) =>
//         merge(of(getUsersFailure(error.message)),
//             caught
//         ))
// );