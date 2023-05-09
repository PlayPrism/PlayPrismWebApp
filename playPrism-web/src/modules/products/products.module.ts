import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { ProductsRoutingModule } from './products-routing.module';
import { ProductsService } from '../../core/services';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './components/product-details/product-details.component';

@NgModule({
  declarations: [HomeComponent, ProductDetailsComponent],
  imports: [ProductsRoutingModule, SharedModule],
  providers: [ProductsService],
})
export class ProductsModule {}
