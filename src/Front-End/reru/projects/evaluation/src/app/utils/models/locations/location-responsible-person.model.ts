export class LocationResponsiblePerson {
    locationId: number;
    userProfileId: number;

    constructor(model?: LocationResponsiblePerson) {
        if (model) {
            this.locationId = model.locationId;
            this.userProfileId = model.userProfileId;
        } else {
            this.locationId = null;
            this.userProfileId = null;
        }
    }
}
