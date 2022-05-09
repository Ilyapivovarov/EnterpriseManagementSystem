import {Account} from "../../types/accountTypes";
import {Session} from "../../types/authTypes";

export interface AuthState {
    currentSession: Session | null ,
    isAuth: boolean,
    error: string | null
}