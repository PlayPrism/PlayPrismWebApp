import {Component, OnInit} from '@angular/core';
import {Observable} from "rxjs";
import {HistoryItem} from "../../../../core/models/history-item";
import {UserService} from "../../../../core/services";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-purchase-history',
  templateUrl: './purchase-history.component.html',
  styleUrls: ['./purchase-history.component.scss']
})
export class PurchaseHistoryComponent implements OnInit{

  public historyItems$: Observable<HistoryItem[]>;
  // public historyItems$: Observable<HistoryItem>;

  public items: HistoryItem[];

  constructor(
    private readonly userService: UserService,
    private route: ActivatedRoute
  ) {
    this.items = [
      {
        productId: "1",
        userid: "user1",
        name: "Cyberpunk 2077",
        rating: 4,
        price: 1750,
        headerImage: "cyberpunk_logo.png",
        purchaseDate: new Date(),
        value: "Value 1"
      },
      {
        productId: "1",
        userid: "user1",
        name: "Cyberpunk 2077",
        rating: 4,
        price: 1750,
        headerImage: "cyberpunk_logo.png",
        purchaseDate: new Date(),
        value: "Value 1"
      },
      {
        productId: "1",
        userid: "user1",
        name: "Cyberpunk 2077",
        rating: 4,
        price: 1750,
        headerImage: "cyberpunk_logo.png",
        purchaseDate: new Date(),
        value: "Value 1"
      },
      {
        productId: "1",
        userid: "user1",
        name: "Cyberpunk 2077",
        rating: 4,
        price: 1750,
        headerImage: "cyberpunk_logo.png",
        purchaseDate: new Date(),
        value: "Value 1"
      },
      {
        productId: "1",
        userid: "user1",
        name: "Cyberpunk 2077",
        rating: 4,
        price: 1750,
        headerImage: "cyberpunk_logo.png",
        purchaseDate: new Date(),
        value: "Value 1"
      },


    ];
  }


  ngOnInit(): void {
    this.historyItems$ = this.userService.getUserHistory();
  }
}
