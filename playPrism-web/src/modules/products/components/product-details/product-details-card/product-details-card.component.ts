import { Component, Input } from '@angular/core';
import { Platform, platformsIcons } from 'src/core/enums';
import { Product } from 'src/core/models';
import { CartItem } from 'src/core/models/cart';
import { CartService } from 'src/core/services';

@Component({
  selector: 'app-product-details-card',
  templateUrl: './product-details-card.component.html',
  styleUrls: ['./product-details-card.component.scss'],
})
export class ProductDetailsCardComponent {
  @Input() product: Product;
  public addedToCart = false;
  public platforms = {
    steam: Platform.Steam,
    epicGames: Platform.EpicGames,
    xbox: Platform.Xbox,
    playStation: Platform.PlayStation,
  };

  constructor(private readonly cartService: CartService) {}

  public AddToCart(): void {
    const productItem: CartItem = {
      id: this.product.id,
      title: this.product.name,
      price: this.product.price,
      headerimage: this.product.headerImage,
      platforms: this.product.platforms,
    };

    this.cartService.addItem(productItem);
    this.addedToCart = true;
  }

  getPlatformIcon(platform: string): string {
    return platformsIcons(platform);
  }
}
