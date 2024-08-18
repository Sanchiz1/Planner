import { Action, PayloadAction } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { catchError, map, merge, mergeMap, Observable, of } from "rxjs";
import { Login, LoginType } from "../../API/AccountRequests";
import { ACCESS_TOKEN_STORAGE_NAME } from "../../config";
import { login, loginFailure, loginSuccess, logout } from "./authSlice";
import { LoginPayload } from "./types/loginPayload";

export const loginEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(login.type),
    mergeMap((action: PayloadAction<LoginPayload>) =>
        Login(action.payload.email, action.payload.password).pipe(
            map((res: LoginType) => {
                localStorage.setItem(ACCESS_TOKEN_STORAGE_NAME, JSON.stringify(
                    res.value
                ));
                return loginSuccess();
            }
            )
        )),
    catchError((error, caught) =>
        merge(of(loginFailure(error.message)),
            caught
        ))
);

export const logoutEpic = (action$: Observable<Action>) => action$.pipe(
    ofType("LOGOUT"),
    map(() => {
        localStorage.removeItem(ACCESS_TOKEN_STORAGE_NAME);
        return logout();
    })
);