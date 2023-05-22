import { Component } from '@angular/core';
import { tap } from 'rxjs';
import { platformsIcons } from 'src/core/enums';
import { CartItem } from 'src/core/models';
import { CartService } from 'src/core/services';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent {
  cart$ = this.cartService.cart$
    .pipe(
      tap((cart) => {
        this.cart = cart;
        console.log(cart);
      })
    )
    .subscribe();
  totalCartPrice$ = this.cartService.totalCartPrice$;
  public cart: CartItem[];

  constructor(private readonly cartService: CartService) {}

  public removeItem(item: CartItem): void {
    this.cartService.removeItem(item);
  }

  getPlatformIcon(platform: string): string {
    return platformsIcons(platform);
  }
}
