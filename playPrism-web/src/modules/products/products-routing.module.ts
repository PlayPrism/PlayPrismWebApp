import { NgModule } from '@angular/core';
import { Route, RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { ShellComponent } from '../shared/components/app-shell/app-shell.component';

export const productsRoute: Route = {
  path: '',
  component: ShellComponent,
  children: [
    { path: '', component: HomeComponent },
    { path: ':id', component: ProductDetailsComponent },
  ],
};

const routes: Routes = [productsRoute];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule {}
