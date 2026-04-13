import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthStoreService {
    private nameId$ = new BehaviorSubject<string >("");
    private email$ = new BehaviorSubject<string>("");
    private isLogged$ = new BehaviorSubject<boolean>(false);

    getIsLogged() {
        return this.isLogged$.asObservable();
    }
    setIsLogged(isLogged: boolean) {
        this.isLogged$.next(isLogged);
    }
    getNameId() {
        return this.nameId$.asObservable();
    }
    setNameId(nameId: string) {
        this.nameId$.next(nameId);
    }
    getEmail() {
        return this.email$.asObservable();
    }
    setEmail(email: string) {
        this.email$.next(email);
    }
    clear() {
    this.isLogged$.next(false);
    this.nameId$.next("");
    this.email$.next("");
  }
}