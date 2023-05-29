import { Dialog } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { SignInModalComponent } from '..';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  constructor(private readonly dialog: Dialog) {}

  public openHeaderDropdown() {
    this.dialog.open(SignInModalComponent, {});
  }
}
