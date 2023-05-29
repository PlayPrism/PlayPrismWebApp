import { NgModule } from '@angular/core';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { AuthRoutingModule } from './auth-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AccountService } from 'src/core/services';
import { SignInComponent } from './components/sign-in/sign-in.component';

@NgModule({
  declarations: [SignUpComponent, SignInComponent],
  imports: [AuthRoutingModule, SharedModule],
  providers: [AccountService],
})
export class AuthModule {}
