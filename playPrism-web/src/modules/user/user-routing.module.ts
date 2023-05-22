import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Route, Routes, RouterModule } from '@angular/router';
import { ShellComponent } from '../shared/components';
import { ProfileComponent } from './components/profile/profile.component';
import { CartComponent } from './components/cart/cart.component';

export const usersRoute: Route = {
  path: '',
  component: ShellComponent,
  children: [
    { path: '', component: ProfileComponent },
    { path: 'cart', component: CartComponent },
  ],
};

const routes: Routes = [usersRoute];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRoutingModule {}
