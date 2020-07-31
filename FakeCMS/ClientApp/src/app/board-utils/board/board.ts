import {State} from '../state/state';
import {DataObject} from '../data-object/data-object';

export class Board {
	id: number;
	name: string;
	states: State[];
  	dataObjects: DataObject[];
}