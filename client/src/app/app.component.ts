import { Component } from '@angular/core';
import {AccountService} from "./account/account.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';

  constructor(private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    if (token){
      this.accountService.setTokenInfo(token);
    }
  }
}
