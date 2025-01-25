import { HttpClient } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { BehaviorSubject, defer, from, Observable, pipe } from 'rxjs';
import { IUser } from '../models/IUser';

const defaultPath = '/';

export type AuthUser = IUser | null | undefined;

interface AuthState {
  user: AuthUser;
}


@Injectable()
export class AuthService {

  private auth = inject(AUTH);

  // sources
  private user$ = authState(this.auth);

  // state
  private state = signal<AuthState>({
    user: undefined,
  });

  // selectors
  user = computed(() => this.state().user);

  http: HttpClient = inject(HttpClient);
  
  private currentUserSubject: BehaviorSubject<IUser | null>;
  public currentUser: Observable<IUser | null>;
  get loggedIn(): boolean {
    return !!this.currentUserSubject.value;
  }

  private _lastAuthenticatedPath: string = defaultPath;
  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  constructor(private router: Router) { 
    const localUser = localStorage.getItem('currentUser');

    if (localUser) 
    {
      this.currentUserSubject = new BehaviorSubject<IUser | null>(JSON.parse(localUser));
    }
    else   
      this.currentUserSubject = new BehaviorSubject<IUser | null>(null);

    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue() {
    return this.currentUserSubject.value;
  }

  public reAuthenticate() {
    if(this.currentUserSubject.value) {
      return this.logIn(this.currentUserSubject.value.username, this.currentUserSubject.value.password);
    } 
    return null; 
  }

  async logIn(userName: string, password: string) {
      return this.http.post<any>(`${environment.baseApiUrk}/users/authenticate`, {Username: userName, Password: password}).toPromise().then( (res: IUser) => {
        if(res) {
          this.router.navigate([this._lastAuthenticatedPath]);
          localStorage.setItem('currentUser', JSON.stringify(res));
          this.currentUserSubject.next(res);
          return {
            isOk: true,
            data: res
          };
        }
        return {
          isOk: false,
          message: "Authentication failed"
        };
      }, error => {
        return {
          isOk: false,
          message: "Authentication failed"
        };
      });
  }

  async createAccount(email: string, password: string) {
    try {

      this.router.navigate(['/create-account']);
      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to create account"
      };
    }
  }

  async changePassword(email: string, recoveryCode: string) {
    try {
      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to change password"
      }
    };
  }

  async resetPassword(email: string) {
    try {
      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to reset password"
      };
    }
  }

  async logOut() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login-form']);
  }
}

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isLoggedIn = this.authService.loggedIn;
    const isAuthForm = [
      'login-form',
    ].includes(route.routeConfig?.path || defaultPath);

    if (isLoggedIn && isAuthForm) {
      this.authService.lastAuthenticatedPath = defaultPath;
      this.router.navigate([defaultPath]);
      return false;
    }

    if (!isLoggedIn && !isAuthForm) {
      this.router.navigate(['/login-form']);
    }

    if (isLoggedIn) {
      this.authService.lastAuthenticatedPath = route.routeConfig?.path || defaultPath;
    }

    return isLoggedIn || isAuthForm;
  }
}
