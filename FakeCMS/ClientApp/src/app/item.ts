export class Item{
    constructor(
        public name: string,
        public description: string,
        public value: number,
        public count: number,
        public id?: number){ }

    static empty(){
        return new Item(null, null, 0, 0);
    }
}