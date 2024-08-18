import { Membership } from "../Types/Memership";
import { GetAjaxObservable } from "./APIUtils";

export function GetMemerWorkspaces() {
    return GetAjaxObservable<Membership[]>(`/workspace/member`, "GET", false, { 'Content-Type': 'application/json' });
}
