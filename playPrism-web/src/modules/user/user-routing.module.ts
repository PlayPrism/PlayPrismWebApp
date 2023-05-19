import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Route, Routes, RouterModule } from '@angular/router';
import { ShellComponent } from '../shared/components';
import { ProfileComponent } from './components/profile/profile.component';

export const productsRoute: Route = {
  path: '',
  component: ShellComponent,
  children: [{ path: '', component: ProfileComponent }],
};

const routes: Routes = [productsRoute];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRoutingModule {}
