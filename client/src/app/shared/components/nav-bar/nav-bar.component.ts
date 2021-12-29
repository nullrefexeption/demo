import { Component, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {User} from "../../models/user.model";
import {AccountService} from "../../../account/account.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  public userName: any;
  currentUser$!: Observable<User | null>;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
    this.getUserName();
    this.accountService.roleChange.subscribe(() => {
      this.getUserName();
    });
  }

  public getUserName() {
    this.userName = this.accountService.userName;
    console.log(this.userName);
  }

  public logout() {
    this.accountService.logout();
  }
}
