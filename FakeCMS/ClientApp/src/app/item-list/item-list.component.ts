import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'item-list',
  templateUrl: './item-list.component.html'
})
export class ItemListComponent {
  public items: Item[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Item[]>(baseUrl + "api/item").subscribe(result => {
      this.items = result;
    })
  }
}

interface Item {
  name: string;
  description: string;
  value: number;
  count: number;
}
