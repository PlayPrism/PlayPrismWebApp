import { NgModule } from '@angular/core';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { AuthRoutingModule } from './auth-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AuthService } from 'src/core/services';

@NgModule({
  declarations: [SignUpComponent],
  imports: [AuthRoutingModule, SharedModule],
  providers: [AuthService],
})
export class AuthModule {}
