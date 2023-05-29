import { Dialog } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SignInModalComponent } from 'src/modules/shared/components';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent {
  constructor(private router: Router, private dialog: Dialog) {}

  public moveToSignInModal() {
    this.router.navigate(['../products']).then(() => {
      this.dialog.open(SignInModalComponent);
    });
  }
}
