import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { ProductsRoutingModule } from './products-routing.module';
import { ProductsService } from '../../core/services';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { ProductCartComponent } from './components/product-cart/product-cart.component';

@NgModule({
  declarations: [HomeComponent, ProductDetailsComponent, ProductCartComponent],
  imports: [ProductsRoutingModule, SharedModule],
  providers: [ProductsService],
})
export class ProductsModule {}
