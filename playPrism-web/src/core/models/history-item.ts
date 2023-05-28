import {baseResponse} from "./baseResponse";

export interface HistoryItem{
  productId: string;
  userid: string;
  name: string;
  rating: number;
  price: number;
  headerImage: string;
  purchaseDate: Date;
  value: string;
}
