import { NgModule } from '@angular/core';
import { Route, RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { ShellComponent } from '../shared/components/app-shell/app-shell.component';
import { CartComponent } from './components';

export const productsRoute: Route = {
  path: 'games',
  component: ShellComponent,
  children: [
    { path: 'cart', component: CartComponent },
    { path: ':id', component: ProductDetailsComponent },
    { path: '', component: HomeComponent },
  ],
};

const routes: Routes = [productsRoute];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule {}
