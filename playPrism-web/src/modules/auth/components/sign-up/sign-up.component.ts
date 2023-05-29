import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { catchError, of, tap } from 'rxjs';
import { AccountService, tokenStorageKey } from 'src/core/services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
})
export class SignUpComponent {
  public isLoading: boolean;
  public alreadyRegistered: string = '';

  public form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    agreeTerms: [false, Validators.requiredTrue],
  });

  constructor(
    private readonly fb: FormBuilder,
    private readonly accountService: AccountService,
    private readonly router: Router
  ) {}

  public get emailControl() {
    return this.form.controls.email;
  }

  public get passwordControl() {
    return this.form.controls.password;
  }

  public get agreeTermsControl() {
    return this.form.controls.agreeTerms;
  }

  onSubmit(): void {
    this.isLoading = true;
    this.accountService
      .register({
        email: this.emailControl.value!,
        password: this.passwordControl.value!,
      })
      .pipe(
        catchError((response) => {
          if (response.status === 401) {
            this.emailControl.setErrors({ alreadyRegistered: true });
            this.alreadyRegistered = response.error.error;
          }
          return of(null);
        }),
        tap((authObject) => {
          if (authObject?.accessToken) {
            localStorage.setItem(tokenStorageKey, JSON.stringify(authObject));
            this.router.navigate(['']);
          }
        }),
        tap(() => (this.isLoading = false))
      )
      .subscribe();
  }
}
