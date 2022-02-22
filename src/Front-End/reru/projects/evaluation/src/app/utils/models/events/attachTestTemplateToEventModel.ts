export class AttachTestTemplateToEventModel {
    eventId: number;
    testTemplateId: number;
    maxAttempts?: number;

    constructor(model?: AttachTestTemplateToEventModel) {
        if (model) {
            this.eventId = model.eventId;
            this.testTemplateId = model.testTemplateId;
            this.maxAttempts = model.maxAttempts;
        } else {
            this.eventId = null;
            this.testTemplateId = null;
            this.maxAttempts = null;
        }
    }
}