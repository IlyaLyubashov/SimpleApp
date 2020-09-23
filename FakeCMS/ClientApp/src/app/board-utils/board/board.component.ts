import { Component, Input, OnInit, OnDestroy, AfterViewInit, ElementRef, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from '../board/board';
import { State } from '../state/state';
import { DataObject } from '../data-object/data-object';
import { BoardService } from './board.service';
import { StateService } from '../state/state.service';
import { StateComponent } from '../state/state.component';
import { OrderBy } from '../pipes/orderby.pipe';
import { Where } from '../pipes/where.pipe';
import { Router, Params, ActivatedRoute, Data } from '@angular/router';
import { ThrowStmt } from '@angular/compiler';
import { YesNoModalComponent } from '../../shared/yes-no-modal/yes-no-modal.component';

declare var jQuery: any;
var curYPos = 0,
  curXPos = 0,
  curDown = false;

@Component({
  selector: 'board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {
  board: Board;
  addingColumn = false;
  addColumnText: string;
  editingTilte = false;
  currentTitle: string;
  boardWidth: number;
  columnsAdded: number = 0;

  constructor(public el: ElementRef,
    private _boardService: BoardService,
    private _columnService: StateService,
    private _router: Router,
    private _route: ActivatedRoute) {
  }

  @ViewChild('consoleModal', { static: true }) public consoleModal: YesNoModalComponent;
  console(data: any){
    this.consoleModal.showAsync(data).then(r => console.log("Modal"), r => {});
  }

  ngOnInit() {
    let boardId = this._route.snapshot.params['id'];
    this._boardService.getBoardWithColumnsAndCards(boardId)
      .subscribe(data => {
        this.board = data[0] as Board;
        this.board.states = data[1] as State[];
        this.board.dataObjects = data[2] as DataObject[];
        this.addNullState();
        document.title = this.board.name;
        this.setupView();  
      });
  }

  addNullState(){
    let nullState = <State>{
      id: null,
      name: "Unstated",
      index : -1
    }
    this.board.states.push(nullState);
  }

  setupView() {
    let component = this;
    setTimeout(function () {
      var startColumn;
      jQuery('#main').sortable({
        items: '.sortable-column',
        handler: '.header',
        connectWith: "#main",
        placeholder: "column-placeholder",
        dropOnEmpty: true,
        tolerance: 'pointer',
        start: function (event, ui) {
          ui.placeholder.height(ui.item.find('.column').outerHeight());
          startColumn = ui.item.parent();
        },
        stop: function (event, ui) {
          var columnId = ui.item.find('.column').attr('column-id');

          component.updateColumnOrder({
            columnId: columnId
          });
        }
      }).disableSelection();

      //component.bindPane();;

      window.addEventListener('resize', function (e) {
        component.updateBoardWidth();
      });
      component.updateBoardWidth();
      document.getElementById('content-wrapper').style.backgroundColor = '';
    }, 100);
  }

  bindPane() {
    let el = document.getElementById('content-wrapper');
    el.addEventListener('mousemove', function (e) {
      e.preventDefault();
      if (curDown === true) {
        el.scrollLeft += (curXPos - e.pageX) * .25;// x > 0 ? x : 0;
        el.scrollTop += (curYPos - e.pageY) * .25;// y > 0 ? y : 0;
      }
    });

    el.addEventListener('mousedown', function (e) {
      let htmlSrcElement = e.srcElement as HTMLElement;
      if (htmlSrcElement.id === 'main' || htmlSrcElement.id === 'content-wrapper') {
        curDown = true;
      }
      curYPos = e.pageY; curXPos = e.pageX;
    });
    el.addEventListener('mouseup', function (e) {
      curDown = false;
    });
  }

  updateBoardWidth() {
    this.boardWidth = ((this.board.states.length + (this.columnsAdded > 0 ? 1 : 2)) * 280) + 10;
    //this.boardWidth = ((this.board.states.length + 1) * 280) + 10;

    if (this.boardWidth > document.body.scrollWidth) {
      document.getElementById('main').style.width = this.boardWidth + 'px';
    } else {
      document.getElementById('main').style.width = '100%';
    }
    
    if (this.columnsAdded > 0) {
      let wrapper = document.getElementById('content-wrapper');
      wrapper.scrollLeft = wrapper.scrollWidth;
    }

    this.columnsAdded++;
  }

  updateBoard() {
    // if (this.board.name && this.board.name.trim() !== '') {
    //   this._boardService.put(this.board);
    // } else {
    //   this.board.title = this.currentTitle;
    // }
    // this.editingTilte = false;
    // document.title = this.board.title + " | Generic Task Manager";
  }

  editTitle() {
    // this.currentTitle = this.board.title;
    // this.editingTilte = true;

    // let input = this.el.nativeElement
    //   .getElementsByClassName('board-title')[0]
    //   .getElementsByTagName('input')[0];

    // setTimeout(function () { input.focus(); }, 0);
  }

  updateColumnElements(column: State) {
    let columnArr = jQuery('#main .column');
    let columnEl = jQuery('#main .column[columnid=' + column.id + ']');
    let i = 0;
    for (; i < columnArr.length - 1; i++) {
      column.index < +columnArr[i].getAttibute('column-order');
      break;
    }

    columnEl.remove().insertBefore(columnArr[i]);
  }

  updateColumnOrder(event) {
    let i: number = 0,
      elBefore: number = -1,
      elAfter: number = -1,
      newOrder: number = 0,
      columnEl = jQuery('#main'),
      columnArr = columnEl.find('.column');

    for (i = 0; i < columnArr.length - 1; i++) {
      if (columnEl.find('.column')[i].getAttribute('column-id') == event.columnId) {
        break;
      }
    }

    if (i > 0 && i < columnArr.length - 1) {
      elBefore = +columnArr[i - 1].getAttribute('column-order');
      elAfter = +columnArr[i + 1].getAttribute('column-order');

      newOrder = elBefore + ((elAfter - elBefore) / 2);
    }
    else if (i == columnArr.length - 1) {
      elBefore = +columnArr[i - 1].getAttribute('column-order');
      newOrder = elBefore + 1000;
    } else if (i == 0) {
      elAfter = +columnArr[i + 1].getAttribute('column-order');

      newOrder = elAfter / 2;
    }

    let column = this.board.states.find(x => x.id === +event.columnId);
    console.log(column)
    column.index = newOrder;
    this._columnService.put(column).subscribe(res => {
      // this._ws.updateColumn(this.board._id, column);
    });
  }


  blurOnEnter(event) {
    if (event.keyCode === 13) {
      event.target.blur();
    }
  }

  enableAddColumn() {
    this.addingColumn = true;
    let input = jQuery('.add-column')[0]
      .getElementsByTagName('input')[0];

    setTimeout(function () { input.focus(); }, 0);
  }

  addColumn() {
    let newColumn = <State>{
      id: -1,
      name: this.addColumnText,
      index: (this.board.states.length + 1) /** 1000*/,
      tableId: this.board.id
    };
    this._columnService.post(newColumn)
      .subscribe(column => {
        console.log("add new column");
        this.board.states.push(column);
        this.updateBoardWidth();
        this.addColumnText = '';
        //this._ws.addColumn(this.board._id, column);
      });
  }

  addColumnOnEnter(event: KeyboardEvent) {
    if (event.keyCode === 13) {
      if (this.addColumnText && this.addColumnText.trim() !== '') {
        this.addColumn();
      } else {
        this.clearAddColumn();
      }
    }
    else if (event.keyCode === 27) {
      this.clearAddColumn();
    }
  }

  addColumnOnBlur() {
    if (this.addColumnText && this.addColumnText.trim() !== '') {
      this.addColumn();
    }
    this.clearAddColumn();
  }

  clearAddColumn() {
    this.addingColumn = false;
    this.addColumnText = '';
  }


  addDataObject(dataObj: DataObject) {
    this.board.dataObjects.push(dataObj);
  }

  deleteState(state: State){
    console.log(state);
    this._columnService.delete(state).subscribe(r => {
      let idx = this.board.states.findIndex(s => s.id == state.id);
      this.board.states.splice(idx, 1);
      this.board.dataObjects.filter(o => o.stateId == state.id)
        .forEach(o => o.stateId = null);
    })
  }

  // foreceUpdateCards() {
  //   var cards = JSON.stringify(this.board.dataObjects);
  //   this.board.dataObjects = JSON.parse(cards);
  // }


  // onCardUpdate(card: Card) {
  //   this.foreceUpdateCards();
  // }
}