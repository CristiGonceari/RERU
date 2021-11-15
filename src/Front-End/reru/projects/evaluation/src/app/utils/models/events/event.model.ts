export class Event {
    id?: number;
	name: string;
	description: string;
    fromDate: string;
    tillDate: string;

    constructor(model?: Event) {
        if (model) {
            this.id = model.id;
            this.name = model.name;
            this.fromDate = model.fromDate;
            this.tillDate = model.tillDate;
            this.description = model.description;
        } else {
            this.id = null;
            this.name = null;
            this.fromDate = null;
            this.tillDate = null;
            this.description = null;
        }
    }
}
