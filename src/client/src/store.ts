import { Action, configureStore, Tuple } from "@reduxjs/toolkit";
import { useDispatch, useSelector } from "react-redux";
import { combineEpics, createEpicMiddleware, Epic } from "redux-observable";
import { getUserEpic } from "./features/account/accountEpics";
import accountReducer from "./features/account/accountSlice";
import usersReducer from "./features/addWorkspaceMember/usersReducerSlice";
import { loginEpic, logoutEpic } from "./features/auth/authEpics";
import authReducer from "./features/auth/authSlice";
import { getMyWorkspacesEpic } from "./features/myWorkspaces/myWorkspacesEpics";
import myWorkspacesReducer from "./features/myWorkspaces/myWorkspacesSlice";
import { createWorkspaceEpic, deleteWorkspaceEpic, getWorkspaceEpic, updateWorkspaceEpic } from "./features/workspace/workspaceEpics";
import workspaceReducer from "./features/workspace/workspaceSlice";
import { getWorkspaceMembersEpic, removeWorkspaceMemberEpic, updateWorkspaceMemberEpic } from "./features/workspaceMembers/workspaceMembersEpics";
import workspaceMembersReducer, { removeWorkspaceMember } from "./features/workspaceMembers/workspaceMembersSlice";
import workspaceMembershipReducer from "./features/workspaceMembership/workspaceMembershipSlice";
import { getWorkspaceMembershipEpic } from "./features/workspaceMembership/workspaceMembershipEpics";
import { addWorkspaceMemberEpic, getUsersEpic } from "./features/addWorkspaceMember/usersEpics";


const rootEpic: Epic<Action, Action, void, any> = combineEpics<Action, Action, void, any>(
    loginEpic,
    logoutEpic,
    getUserEpic,
    getMyWorkspacesEpic,
    getWorkspaceEpic,
    updateWorkspaceEpic,
    deleteWorkspaceEpic,
    createWorkspaceEpic,
    getWorkspaceMembershipEpic,
    getWorkspaceMembersEpic,
    removeWorkspaceMemberEpic,
    updateWorkspaceMemberEpic,
    getUsersEpic,
    addWorkspaceMemberEpic
);

const epicMiddleware = createEpicMiddleware<Action, Action, void, any>();


export const store = configureStore({
    reducer: {
        auth: authReducer,
        account: accountReducer,
        myWorkspaces: myWorkspacesReducer,
        workspace: workspaceReducer,
        workspaceMembership: workspaceMembershipReducer,
        workspaceMembers: workspaceMembersReducer,
        users: usersReducer
    },
    middleware: () => new Tuple(epicMiddleware)
})

epicMiddleware.run(rootEpic);

export const useAppDispatch = useDispatch.withTypes<AppDispatch>()
export const useAppSelector = useSelector.withTypes<RootState>()

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;