import { Action } from "@reduxjs/toolkit";
import { ofType } from "redux-observable";
import { map, Observable } from "rxjs";
import { ACCESS_TOKEN_STORAGE_NAME } from "../../config";
import { getUser, getUserFailure, getUserSuccess } from "./accountSlice";

export const getUserEpic = (action$: Observable<Action>) => action$.pipe(
    ofType(getUser.type),
    map(() => {
        const token =localStorage.getItem(ACCESS_TOKEN_STORAGE_NAME) ?? null;

        return token ? getUserSuccess(token) : getUserFailure("Invalid token");
    })
);