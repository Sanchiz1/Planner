import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, concatAll, map, merge, mergeMap, Observable, of } from "rxjs";
import { CreateWorkspace, DeleteWorkspace, GetWorkspace, UpdateWorkspace } from "../../API/WorkspaceRequests";
import { Workspace } from "../../Types/Workspace";
import { createWorkspace, createWorkspaceFailure, createWorkspaceSuccess, deleteWorkspace, deleteWorkspaceFailure, deleteWorkspaceSuccess, getWorkspace, getWorkspaceFailure, getWorkspaceSuccess, updateWorkspace, updateWorkspaceFailure, updateWorkspaceSuccess } from "./workspaceSlice";
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
            mergeMap(() => merge(
                of(updateWorkspaceSuccess()),
                of(getWorkspace(action.payload.id))
            )),
            catchError((error) => of(updateWorkspaceFailure(error.message)))
        )
    )
);

export const deleteWorkspaceEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(deleteWorkspace.type),
    mergeMap((action: PayloadAction<number>) =>
        DeleteWorkspace(action.payload).pipe(
            mergeMap(() => merge(
                of(deleteWorkspaceSuccess()),
                of(getWorkspace(action.payload))
            )),
            catchError((error) => of(deleteWorkspaceFailure(error.message)))
        )
    )
);

export const createWorkspaceEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(createWorkspace.type),
    mergeMap((action: PayloadAction<Workspace>) =>
        CreateWorkspace(action.payload.name, action.payload.isPublic).pipe(
            mergeMap((workspaceId: number) => merge(
                of(createWorkspaceSuccess()),
                of(getWorkspaceSuccess({ ...action.payload, id: workspaceId }))
            )),
            catchError((error) => of(createWorkspaceFailure(error.message)))
        )
    )
);