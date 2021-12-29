import {Directive, ElementRef, Input, OnInit, TemplateRef, ViewContainerRef} from '@angular/core';
import {AccountService} from "../../account/account.service";

@Directive({
  selector: '[appShowForRole]'
})
export class ShowForRoleDirective implements OnInit {
  @Input() appShowForRole!: string;

  constructor(el: ElementRef,
              private viewContainerRef: ViewContainerRef,
              private template: TemplateRef<any>,
              private accountService: AccountService) {
  }

  ngOnInit() {
    this.checkUserInRole();
    this.accountService.roleChange.subscribe(() => {
      this.checkUserInRole();
    });
  }

  checkUserInRole() {
    if (!this.accountService.userInRole(this.appShowForRole)) {
      this.viewContainerRef.clear();
    } else {
      this.viewContainerRef.createEmbeddedView(this.template);
    }
  }
}
