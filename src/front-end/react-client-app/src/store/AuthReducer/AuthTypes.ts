import {Account} from "../../types/accountTypes";
import {Session} from "../../types/authTypes";

export interface AuthState {
    currentSession: Session | null,
    error?: string | null,
    isLoading: boolean
}