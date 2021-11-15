export class AttachTestTypeToEventModel {
    eventId: number;
    testTypeId: number;
    maxAttempts?: number;

    constructor(model?: AttachTestTypeToEventModel) {
        if (model) {
            this.eventId = model.eventId;
            this.testTypeId = model.testTypeId;
            this.maxAttempts = model.maxAttempts;
        } else {
            this.eventId = null;
            this.testTypeId = null;
            this.maxAttempts = null;
        }
    }
}