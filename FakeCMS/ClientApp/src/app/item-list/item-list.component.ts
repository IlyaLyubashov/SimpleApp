import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'
import { ItemService } from '../item.service';
import { Item } from '../item';

import { faMinusCircle } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css'],
  providers: [ItemService]
})
export class ItemListComponent implements OnInit{
  faMinusCircle = faMinusCircle;

  public items: Item[];
  public editMode : boolean;
  public deleteMode : boolean;

  constructor( private _itemService : ItemService, 
              private _router: Router) {}


  ngOnInit(){
    this._itemService.list()
      .subscribe(items => this.items = items);
  }
  

  toogleEditMode(){
    this.editMode = !this.editMode;
  }

  toogleDeleteMode(){
    this.deleteMode = !this.deleteMode;
  }

  selectItem(item : Item){
    if(this.editMode)
      this._router.navigate(['item-details', item.id])
  }
  
  async deleteItem(item : Item){
    await this._itemService.delete(item.id).toPromise()
    this._itemService.list()
      .subscribe(items => this.items = items)
  }
}

