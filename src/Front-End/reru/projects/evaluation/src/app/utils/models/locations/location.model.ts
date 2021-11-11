import { TestingLocationTypeEnum } from "../../enums/testing-location-type.enum";

export class LocationModel {
    id?: number;
    name: string;
    address: string;
    places: number;
    description: string;
    testingLocationType: TestingLocationTypeEnum;
}
