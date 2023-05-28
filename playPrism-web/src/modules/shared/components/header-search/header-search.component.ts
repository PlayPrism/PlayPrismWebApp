import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import {
  debounceTime,
  distinctUntilChanged,
  filter,
  fromEvent,
  switchMap,
  tap,
} from 'rxjs';
import { SearchItem } from 'src/core/models';
import { ProductsService } from 'src/core/services';

@Component({
  selector: 'app-header-search',
  templateUrl: './header-search.component.html',
  styleUrls: ['./header-search.component.scss'],
})
export class HeaderSearchComponent {
  @ViewChild('search') input: ElementRef;

  public items: SearchItem[] = [];
  public showDropdown = false;

  public form = this.fb.group({
    search: [''],
  });

  constructor(
    private readonly fb: FormBuilder,
    private readonly productService: ProductsService
  ) {}

  ngAfterViewInit() {
    // server-side search
    fromEvent(this.input.nativeElement, 'keyup')
      .pipe(
        filter(Boolean),
        debounceTime(150),
        distinctUntilChanged(),
        switchMap((event: any) => {
          const keyword = event.target.value.trim();

          if (keyword.length > 0) {
            return this.productService.getSearchableItems(keyword);
          } else {
            this.showDropdown = false;
            this.items = [];
            return [];
          }
        }),
        tap((items) => {
          this.items = items;
          this.showDropdown = true;
        })
      )
      .subscribe();
  }
}
