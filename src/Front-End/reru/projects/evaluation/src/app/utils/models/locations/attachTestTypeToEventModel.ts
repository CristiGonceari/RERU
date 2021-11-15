export class AttachLocationToEventModel {
    eventId: number;
    locationId: number;

    constructor(model?: AttachLocationToEventModel) {
        if (model) {
            this.eventId = model.eventId;
            this.locationId = model.locationId;
        } else {
            this.eventId = null;
            this.locationId = null;
        }
    }
}