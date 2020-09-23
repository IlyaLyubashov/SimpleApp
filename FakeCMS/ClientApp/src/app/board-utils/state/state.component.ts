import {Component, Input, Output, OnInit, AfterViewInit, EventEmitter, ElementRef} from '@angular/core';
import {State} from './state';
import {DataObject} from '../data-object/data-object';
import {DataObjectComponent} from '../data-object/data-object.component'
import {StateService} from './state.service';
// import {WebSocketService} from '../ws.service';
import {DataObjectService} from '../data-object/data-object.service';
import { Data } from '@angular/router';
import {OrderBy} from '../pipes/orderby.pipe';
import {Where} from '../pipes/where.pipe';
import { faTimes } from '@fortawesome/free-solid-svg-icons';

declare var jQuery: any;

@Component({
  selector: 'gtm-column',
  templateUrl: './state.component.html',
  styleUrls: ['./state.component.css'],
})
export class StateComponent implements OnInit {
  @Input()
  column: State;
  @Input()
  cards: DataObject[];
  @Output()
  public onAddDataObject: EventEmitter<DataObject>;
  @Output() cardUpdate: EventEmitter<DataObject>;

  editingState = false;
  addingDataObject = false;
  addDataObjectText: string;
  currentTitle: string;

  constructor(private el: ElementRef,
    // private _ws: WebSocketService,
    private _stateService: StateService,
    private _dataObjectService: DataObjectService) {
    this.onAddDataObject = new EventEmitter();
    this.cardUpdate = new EventEmitter();
  }

  ngOnInit() {
    console.log(this.cards);
    this.setupView();
    // this._ws.onStateUpdate.subscribe((column: State) => {
    //   if (this.column._id === column._id) {
    //     this.column.title = column.title;
    //     this.column.order = column.order;
    //   }
    // });
  }

  setupView() {
    let component = this;
    var startState;
    jQuery('.card-list').sortable({
      connectWith: ".card-list",
      placeholder: "card-placeholder",
      dropOnEmpty: true,
      tolerance: 'pointer',
      start: function(event, ui) {
        ui.placeholder.height(ui.item.outerHeight());
        startState = ui.item.parent();
      },
      stop: function(event, ui) {
        var senderStateId = startState.attr('column-id');
        var targetStateId = ui.item.closest('.card-list').attr('column-id');
        var cardId = ui.item.find('.card').attr('card-id');
        component.updateDataObjectsOrder({
          columnId: targetStateId || senderStateId,
          cardId: cardId
        });
      }
    });
    jQuery('.card-list').disableSelection();
  }

  updateDataObjectsOrder(event) {
    let cardArr = jQuery('[column-id=' + event.columnId + '] .card'),
      i: number = 0,
      elBefore: number = -1,
      elAfter: number = -1,
      newOrder: number = 0;

    for (i = 0; i < cardArr.length - 1; i++) {
      if (cardArr[i].getAttribute('card-id') == event.cardId) {
        break;
      }
    }

    if (cardArr.length > 1) {
      if (i > 0 && i < cardArr.length - 1) {
        elBefore = +cardArr[i - 1].getAttribute('card-order');
        elAfter = +cardArr[i + 1].getAttribute('card-order');

        newOrder = elBefore + ((elAfter - elBefore) / 2);
      }
      else if (i == cardArr.length - 1) {
        elBefore = +cardArr[i - 1].getAttribute('card-order');
        newOrder = elBefore + 1000;
      } else if (i == 0) {
        elAfter = +cardArr[i + 1].getAttribute('card-order');

        newOrder = elAfter / 2;
      }
    } else {
      newOrder = 1000;
    }

    console.log(event);
    let card = this.cards.filter(x => x.objectId === +event.cardId)[0];
    card.index = newOrder;
    card.stateId = +event.columnId;
    this._dataObjectService.put(card).subscribe(res => {
      // this._ws.updateDataObject(this.column.boardId, card);
    });
  }

  blurOnEnter(event) {
    if (event.keyCode === 13) {
      event.target.blur();
    }
  }

  addStateOnEnter(event: KeyboardEvent) {
    if (event.keyCode === 13) {
      this.updateState();
    } else if (event.keyCode === 27) {
      this.cleadAddState();
    }
  }

  addDataObject() {
    this.cards = this.cards || [];
    let newDataObject = <DataObject>{
      title: this.addDataObjectText,
      index: (this.cards.length + 1) * 1000,
      stateId: this.column.id,
      boardId: this.column.tableId,
      description : '',
      objectId : 0,
      tableId : 0
    };
    this._dataObjectService.post(newDataObject)
      .subscribe(card => {
        this.onAddDataObject.emit(newDataObject);
        //this._ws.addDataObject(card.boardId, card);
      });
  }

  addDataObjectOnEnter(event: KeyboardEvent) {
    if (event.keyCode === 13) {
      if (this.addDataObjectText && this.addDataObjectText.trim() !== '') {
        this.addDataObject();
        this.addDataObjectText = '';
      } else {
        this.clearAddDataObject();
      }
    } else if (event.keyCode === 27) {
      this.clearAddDataObject();
    }
  }

  updateState() {
    if (this.column.name && this.column.name.trim() !== '') {
      this._stateService.put(this.column).subscribe(res => {
        //this._ws.updateState(this.column.boardId, this.column);
      });
      this.editingState = false;
    } else {
      this.cleadAddState();
    }
  }

  cleadAddState() {
    this.column.name = this.currentTitle;
    this.editingState = false;
  }

  editState() {
    this.currentTitle = this.column.name;
    this.editingState = true;
    let input = this.el.nativeElement
      .getElementsByClassName('column-header')[0]
      .getElementsByTagName('input')[0];

    setTimeout(function() { input.focus(); }, 0);
  }

  enableAddDataObject() {
    this.addingDataObject = true;
    let input = this.el.nativeElement
      .getElementsByClassName('add-card')[0]
      .getElementsByTagName('input')[0];

    setTimeout(function() { input.focus(); }, 0);
  }


  updateStateOnBlur() {
    if (this.editingState) {
      this.updateState();
      this.clearAddDataObject();
    }
  }


  addDataObjectOnBlur() {
    if (this.addingDataObject) {
      if (this.addDataObjectText && this.addDataObjectText.trim() !== '') {
        this.addDataObject();
      }
    }
    this.clearAddDataObject();
  }

  clearAddDataObject() {
    this.addingDataObject = false;
    this.addDataObjectText = '';
  }

  onDataObjectUpdate(card: DataObject){
    this.cardUpdate.emit(card);
  }
}