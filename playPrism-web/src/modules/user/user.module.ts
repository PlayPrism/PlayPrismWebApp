import { NgModule } from '@angular/core';
import { UserRoutingModule } from './user-routing.module';
import { ProfileComponent } from './components/profile/profile.component';
import { UserService } from 'src/core/services';
import { SharedModule } from '../shared/shared.module';
import { PurchaseHistoryComponent } from './components/purchase-history/purchase-history.component';
import { UserListgroupComponent } from './components/user-listgroup/user-listgroup.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';

@NgModule({
  declarations: [ProfileComponent, PurchaseHistoryComponent, UserListgroupComponent, ResetPasswordComponent],
  imports: [UserRoutingModule, SharedModule],
  providers: [UserService],
})
export class UserModule {}
