import {Injectable, Output} from '@angular/core';
import {map} from "rxjs/operators";
import {ReplaySubject, Subject} from "rxjs";
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {User} from "../shared/models/user.model";
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  @Output() public userId: string | null = null;
  @Output() public userName: string | null = null;
  @Output() public roles: Array<string> = [];

  private currentUserSource = new ReplaySubject<User | null>(1);

  baseUrl = environment.apiUrl;
  currentUser$ = this.currentUserSource.asObservable();
  roleChange: Subject<Array<string | null>> = new Subject<Array<string | null>>();

  constructor(private http: HttpClient,
              private  router: Router) {
    const token = localStorage.getItem('token');
    if (token) {
      this.setTokenInfo(token);
    }
  }

  login(login: string, password: string) {
    return this.http.post<User>(this.baseUrl + 'accounts/login', {login, password}).pipe(
      map((user: User) => {
        if (user) {
          this.setTokenInfo(user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);

    this.userName = null;
    this.userId = null;

    this.roles = [];
    this.roleChange.next(this.roles);
    this.router.navigateByUrl('/');
  }

  userInRole(roles: string): boolean {
    if (!roles || !this.roles) {
      return false;
    }

    const r = roles.split(',');
    for (let index = this.roles.length - 1; index >= 0; --index) {

      for (let i = 0; i < r.length; i++) {
        if (r[i].replace(' ', '') === this.roles[index]) {
          return true;
        }
      }
    }

    return false;
  }

  setTokenInfo(token: string) {
    const decodedToken = token ? this.getDecodedAccessToken(token) : null;
    this.userName = decodedToken ? decodedToken['email'] : null;
    this.userId = decodedToken ? decodedToken['nameid'] : null;
    const role = decodedToken ? decodedToken['role'] : null;

    if (role) {
      this.roles = Array.isArray(role) ? role : [role];
    } else {
      this.roles = [];
    }

    if (decodedToken) {
        localStorage.setItem('token', token);
    }

    this.roleChange.next(this.roles);
  }

  getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch (Error) {
      return null;
    }
  }
}
