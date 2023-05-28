import { NgModule } from '@angular/core';
import { ProductsService } from 'src/core/services';
import { SharedModule } from '../shared/shared.module';
import {
  CatalogueComponent,
  ProductDetailsComponent,
  CartComponent,
} from './components';
import { ProductDetailsCardComponent } from './components/product-details/product-details-card/product-details-card.component';
import { ProductsRoutingModule } from './products-routing.module';

@NgModule({
  declarations: [
    CatalogueComponent,
    ProductDetailsComponent,
    ProductDetailsCardComponent,
    CartComponent,
  ],
  imports: [ProductsRoutingModule, SharedModule],
  providers: [ProductsService],
})
export class ProductsModule {}
