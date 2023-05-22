export interface Cart {
  items: CartItem[];
}

export interface CartItem {
  id: string;
  title: string;
  headerimage: string;
  price: number;
  platforms?: string[];
}
