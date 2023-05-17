import { NgModule } from '@angular/core';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { AuthRoutingModule } from "./auth-routing.module";
import { SharedModule } from "../shared/shared.module";



@NgModule({
  declarations: [SignUpComponent],
  imports: [AuthRoutingModule, SharedModule]
})
export class AuthModule { }
