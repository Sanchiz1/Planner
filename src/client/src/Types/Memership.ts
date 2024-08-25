import { Role } from "./Role"
import { Workspace } from "./Workspace"

export interface Membership {
    id: number,
    userId: number,
    workspaceId: number,
    roleId: number,
    role: Role,
    workspace: Workspace
}