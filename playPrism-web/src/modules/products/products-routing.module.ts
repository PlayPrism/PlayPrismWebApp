import {Route, RouterModule, Routes} from "@angular/router";
import {ShellComponent} from "../shared/components";
import {CartComponent, CatalogueComponent, MainPageComponent, ProductDetailsComponent} from "./components";
import {NgModule} from "@angular/core";


export const productsRoute: Route = {
  path: 'games',
  component: ShellComponent,
  children: [
    { path: 'cart', component: CartComponent },
    { path: ':id', component: ProductDetailsComponent },
    //{ path: 'catalogue', component: CatalogueComponent },
    //{ path: '', component: MainPageComponent },
    { path: '', component: CatalogueComponent },
  ],
};

const routes: Routes = [productsRoute];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule {}
