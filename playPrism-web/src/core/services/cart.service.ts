import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { CartItem } from '../models/cart';

const cartKey = 'cart';
const cached = localStorage.getItem(cartKey);

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private readonly cartSubject$ = new BehaviorSubject<CartItem[]>(
    cached ? JSON.parse(cached) : []
  );
  public cart$ = this.cartSubject$.asObservable();
  public totalCartPrice$ = this.cart$.pipe(map(() => this.totalPrice));

  private get currentValue(): CartItem[] {
    return this.cartSubject$.value;
  }

  public get totalPrice() {
    return this.cartSubject$.value.reduce((t, item) => t + item.price, 0);
  }

  public addItem(item: CartItem) {
    const cartIndex = this.currentValue.findIndex(
      (cart) => cart.id === item.id
    );

    if (cartIndex !== -1) {
      this.currentValue[cartIndex] = item;
      localStorage.setItem(cartKey, JSON.stringify(this.currentValue));
      this.cartSubject$.next(this.currentValue);
      return;
    }

    const newCart = [...this.currentValue, item];
    localStorage.setItem(cartKey, JSON.stringify(newCart));
    this.cartSubject$.next(newCart);
  }

  public updateItem(item: CartItem) {
    const newCart = this.currentValue.map((e) => (e.id === item.id ? item : e));
    localStorage.setItem(cartKey, JSON.stringify(newCart));
    this.cartSubject$.next(newCart);
  }

  public removeItem(item: CartItem) {
    const itemToRemove = this.currentValue.find((e) => e.id == item.id);

    const newCart = this.currentValue.filter(
      (x) => JSON.stringify(x) != JSON.stringify(itemToRemove)
    );

    localStorage.setItem(cartKey, JSON.stringify(newCart));
    this.cartSubject$.next(newCart);
  }

  public setCart(cart: CartItem[]) {
    localStorage.setItem(cartKey, JSON.stringify(cart));
    this.cartSubject$.next(cart);
  }

  public clearCart(): void {
    localStorage.removeItem(cartKey);
    this.cartSubject$.next([]);
  }
}
