import { Component, OnInit} from '@angular/core';
import { Item } from '../item';
import { ItemService } from '../services/item.service';
import { ActivatedRoute} from '@angular/router';
import { Observable } from 'rxjs';

@Component({
    selector : 'item-details' ,
    templateUrl : './item-details.component.html',
    providers: [ItemService]
})
export class ItemDetailsComponent implements OnInit{
    public readonly itemId : number | undefined;
    
    public item : Item;

    constructor(private _itemService : ItemService,
         activatedRoute : ActivatedRoute) {
        this.itemId = activatedRoute.snapshot.params['id'];
    }


    ngOnInit(){
        if(this.itemId)
            this._itemService.getById(this.itemId).subscribe(item => this.item = item);
        else
            this.item = Item.empty();
    }

    commitChanges() {
        let observable : Observable<Item>;
        if(this.itemId)
            observable = this._itemService.update(this.item);
        else
            observable = this._itemService.create(this.item);
        
        observable.subscribe(null, 
            error => {

            },
            () => {

            }
        );
    }
    


}
