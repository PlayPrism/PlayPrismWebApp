import { NgModule } from '@angular/core';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { AuthRoutingModule } from "./auth-routing.module";
import { SharedModule } from "../shared/shared.module";
import {FormsModule} from "@angular/forms";



@NgModule({
  declarations: [SignUpComponent],
    imports: [AuthRoutingModule, SharedModule, FormsModule]
})
export class AuthModule { }
