import { NgModule } from '@angular/core';
import { UserRoutingModule } from './user-routing.module';
import { ProfileComponent } from './components/profile/profile.component';
import { UserService } from 'src/core/services';
import { SharedModule } from '../shared/shared.module';
import { CartComponent } from './components/cart/cart.component';

@NgModule({
  declarations: [ProfileComponent, CartComponent],
  imports: [UserRoutingModule, SharedModule],
  providers: [UserService],
})
export class UserModule {}
