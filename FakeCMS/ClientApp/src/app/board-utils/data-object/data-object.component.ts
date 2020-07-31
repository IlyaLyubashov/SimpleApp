import {Component, OnInit, Input, Output, EventEmitter, ElementRef, ChangeDetectorRef, NgZone} from '@angular/core';
import {DataObject} from './data-object';
import {State} from '../state/state';
import {DataObjectService} from './data-object.service';
import { Data } from '@angular/router';
//import {WebSocketService} from '../ws.service';

@Component({
  selector: 'gtm-card',
  templateUrl: './data-object.component.html',
  styleUrls: ['./data-object.component.css'],
})
export class DataObjectComponent implements OnInit {
  @Input()
  dataObj: DataObject;
  @Output() cardUpdate: EventEmitter<DataObject>;
  editingDataObject = false;
  currentTitle: string;
  zone: NgZone;
  constructor(private el: ElementRef,
    private _ref: ChangeDetectorRef,
    //private _ws: WebSocketService,
    private _cardService: DataObjectService) {
    this.zone = new NgZone({ enableLongStackTrace: false });
    this.cardUpdate = new EventEmitter();
  }

  ngOnInit() {
    // this._ws.onDataObjectUpdate.subscribe((card: DataObject) => {
    //   if (this.card._id === card._id) {
    //     this.card.title = card.title;
    //     this.card.order = card.order;
    //     this.card.columnId = card.columnId;
    //   }
    // });
  }

  blurOnEnter(event) {
    if (event.keyCode === 13) {
      event.target.blur();
    } else if (event.keyCode === 27) {
      this.dataObj.title = this.currentTitle;
      this.editingDataObject = false;
    }
  }

  editDataObject() {
    this.editingDataObject = true;
    this.currentTitle = this.dataObj.title;

    let textArea = this.el.nativeElement.getElementsByTagName('textarea')[0];

    setTimeout(function() {
      textArea.focus();
    }, 0);
  }

  updateDataObject() {
    if (!this.dataObj.title || this.dataObj.title.trim() === '') {
      this.dataObj.title = this.currentTitle;
    }

    this._cardService.put(this.dataObj).subscribe(res => {
      //this._ws.updateDataObject(this.dataObj.tableId, this.dataObj);
    });
    this.editingDataObject = false;
  }

  //TODO: check lifecycle
  private ngOnDestroy() {
    //this._ws.onDataObjectUpdate.unsubscribe();
  }

}