import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable, of} from 'rxjs';
import {map} from "rxjs/operators";
import {AccountService} from "../../account/account.service";
import {User} from "../../shared/models/user.model";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService,
              private router: Router) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    this.accountService.currentUser$.pipe(
      map((auth: User | null) => {
        if (auth) {
          return true;
        }
        this.router.navigate(['account/login'], {queryParams: {returnUrl: state.url}});
        return false;
      })
    );

    return of(false);
  }
}
