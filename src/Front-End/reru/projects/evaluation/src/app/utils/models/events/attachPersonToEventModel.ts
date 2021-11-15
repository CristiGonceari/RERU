export class AttachPersonToEventModel {
    eventId: number;
    userProfileId: number;

    constructor(model?: AttachPersonToEventModel) {
        if (model) {
            this.eventId = model.eventId;
            this.userProfileId = model.userProfileId;
        } else {
            this.eventId = null;
            this.userProfileId = null;
        }
    }
}