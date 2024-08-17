import { GetAjaxObservable } from "./APIUtils";

export type LoginType = {
    value: string
}

export function Login(email: string, password: string) {
    return GetAjaxObservable<LoginType>(`/account/login`, "POST", false, { 'Content-Type': 'application/json' }, false, {
        'email': email,
        'password': password
    });
}
