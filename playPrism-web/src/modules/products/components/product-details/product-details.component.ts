import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, switchMap } from 'rxjs';
import { Product } from 'src/core/models';
import { ProductsService } from 'src/core/services';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  public product$: Observable<Product>;

  constructor(
    private readonly productService: ProductsService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.product$ = this.route.paramMap.pipe(
      switchMap((params) => {
        const productId = params.get('id');
        if (!productId) {
          throw new Error('Product ID not provided');
        }
        return this.productService.getProducts(productId);
      })
    );
  }
}
