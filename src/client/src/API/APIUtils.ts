import { catchError, map, throwError } from "rxjs";
import { ajax } from "rxjs/internal/ajax/ajax";
import { ACCESS_TOKEN_STORAGE_NAME } from "../config";
import { IsLoggedIn } from "../Helpers/LoggedInHelper";
import NotFoundError from "../Types/NotFoundError";

const url = "https://localhost:7269";

export type ApiErrorType = {
    code: string,
    message: string
}

export function GetAjaxObservable<T>(requestUrl: string,
    method: string,
    needsAuth: boolean,
    headers: any,
    withCredentials: boolean = false,
    body: any = null
) {
    if (needsAuth || IsLoggedIn()) {
        let token = localStorage.getItem(ACCESS_TOKEN_STORAGE_NAME);;

        if (token === null) return throwError(() => new Error("Invalid token"));

        headers = { ...headers, ...{ 'Authorization': 'Bearer ' + token } };
    }

    return ajax<T>({
        url: url + requestUrl,
        method: method,
        headers: headers,
        body: body,
        withCredentials: withCredentials,
    }).pipe(
        map(res =>
            res.response
        ),
        catchError((error) => {
            const apiError: ApiErrorType = error?.response;

            if (error && error.status == 401) {
                throw new Error(apiError.message ?? "Unauthorized")
            }

            if (error && error.status == 404) {
                throw new NotFoundError(apiError.message ?? "Not found");
            }

            throw new Error(apiError.message ?? "Internal error");
        }))
}