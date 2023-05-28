import { Platform } from '../enums';

export interface Product {
  id: string;
  name: string;
  rating: number;
  price: number;
  headerImage: string;
  images: string[];
  shortDescription: string;
  detailedDescription: string;
  releaseDate: Date;
  genres: string[];
  platforms: Platform[];
}
