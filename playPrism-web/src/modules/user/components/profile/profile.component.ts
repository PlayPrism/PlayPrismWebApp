import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/core/services';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  user$ = this.userService.user$;
  public isLoading: boolean = true;

  constructor(private readonly userService: UserService) {}

  public ngOnInit(): void {
    this.userService.getUser().subscribe();
    this.isLoading = false;
  }
}
