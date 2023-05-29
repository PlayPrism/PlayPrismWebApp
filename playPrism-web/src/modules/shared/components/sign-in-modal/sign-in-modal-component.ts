import { Dialog, DialogRef } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in-modal',
  templateUrl: './sign-in-modal-component.html',
  styleUrls: ['./sign-in-modal-component.scss'],
})
export class SignInModalComponent {
  constructor(private router: Router, private readonly dialogRef: DialogRef) {}

  public register() {
    this.close();
    this.router.navigate(['sign-up']);
  }

  public close() {
    this.dialogRef.close();
  }
}
