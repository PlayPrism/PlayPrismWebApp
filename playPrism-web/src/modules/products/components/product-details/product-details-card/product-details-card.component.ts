import { Component, Input } from '@angular/core';
import { Platform } from 'src/core/enums';
import { Product } from 'src/core/models';

@Component({
  selector: 'app-product-details-card',
  templateUrl: './product-details-card.component.html',
  styleUrls: ['./product-details-card.component.scss'],
})
export class ProductDetailsCardComponent {
  @Input() product: Product;

  public platforms = {
    steam: Platform.Steam,
    epicGames: Platform.EpicGames,
    xbox: Platform.Xbox,
    playStation: Platform.PlayStation,
  };

  getPlatformIcon(platform: string): string {
    switch (platform) {
      case Platform.Steam:
        return `assets/icons/platforms/steam-icon.svg`;
      case Platform.PlayStation:
        return `assets/icons/platforms/play-station-icon.svg`;
      case Platform.Xbox:
        return `assets/icons/platforms/xbox-icon.svg`;
      case Platform.EpicGames:
        return `assets/icons/platforms/epic-games-icom.svg`;
      default:
        return '';
    }
  }
}
