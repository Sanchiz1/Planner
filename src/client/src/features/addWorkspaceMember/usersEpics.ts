import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { AddWorkspaceMember, GetWorkspaceMembers } from "../../API/WorkspaceRequests";
import { MembershipUser } from "../../Types/MembershipUser";
import { addWorkspaceMember, addWorkspaceMemberFailure, addWorkspaceMemberSuccess, getUsers, getUsersFailure, getUsersSuccess } from "./usersReducerSlice";
import { User } from "../../Types/User";
import { GetUsers } from "../../API/UserRequests";
import { AddMemberPayload } from "./types/addMemberPayload";

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

export const addWorkspaceMemberEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(addWorkspaceMember.type),
    mergeMap((action: PayloadAction<AddMemberPayload>) =>
        AddWorkspaceMember(action.payload.workspaceId, action.payload.userId, action.payload.roleId).pipe(
            map(() => {
                return addWorkspaceMemberSuccess();
            }
            )
        )),
    catchError((error, caught) =>
        merge(of(addWorkspaceMemberFailure(error.message)),
            caught
        ))
);