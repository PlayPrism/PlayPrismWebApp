import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Route, Routes, RouterModule } from '@angular/router';
import { ShellComponent } from '../shared/components';
import { ProfileComponent } from './components/profile/profile.component';
import {PurchaseHistoryComponent} from "./components/purchase-history/purchase-history.component";

export const usersRoute: Route = {
  path: '',
  component: ShellComponent,
  children: [
    { path: '', component: ProfileComponent },
    {path: 'history', component: PurchaseHistoryComponent}],
};

const routes: Routes = [usersRoute];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRoutingModule {}
