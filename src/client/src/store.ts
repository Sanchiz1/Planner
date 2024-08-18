import {combineEpics, createEpicMiddleware, Epic} from "redux-observable";
import {Action, configureStore, Tuple} from "@reduxjs/toolkit";
import { loginEpic, logoutEpic } from "./features/auth/authEpics";
import authReducer from "./features/auth/authSlice";
import accountReducer from "./features/account/accountSlice";
import myWorkspacesReducer from "./features/myWorkspaces/myWorkspacesSlice";
import workspaceReducer from "./features/workspace/workspaceSlice";
import { useDispatch, useSelector } from "react-redux";
import { getUserEpic } from "./features/account/accountEpics";
import { getMyWorkspacesEpic } from "./features/myWorkspaces/myWorkspacesEpics";
import { getWorkspaceEpic } from "./features/workspace/workspaceEpics";


const rootEpic: Epic<Action, Action, void, any> = combineEpics<Action, Action, void, any>(
    loginEpic,
    logoutEpic,
    getUserEpic,
    getMyWorkspacesEpic,
    getWorkspaceEpic
  );

const epicMiddleware = createEpicMiddleware<Action, Action, void, any>();


export const store = configureStore({
    reducer: {
        auth: authReducer,
        account: accountReducer,
        myWorkspaces: myWorkspacesReducer,
        workspace: workspaceReducer,
    },
    middleware: () => new Tuple(epicMiddleware)
})

epicMiddleware.run(rootEpic);

export const useAppDispatch = useDispatch.withTypes<AppDispatch>()
export const useAppSelector = useSelector.withTypes<RootState>()

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;