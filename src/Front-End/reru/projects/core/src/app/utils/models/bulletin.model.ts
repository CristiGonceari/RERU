export interface BulletinModel {
    id?: number;
    series: string;
    releaseDay: string;
    emittedBy: string;
    userProfileId?: number;
    birthPlace: AddressModel;
    parentsResidenceAddress: AddressModel;
    residenceAddress: AddressModel;
}

export interface AddressModel {
    id?: number;
    country: string;
    region: string;
    city: string;
    street: string;
    building: string;
    apartment: string;
    postCode: string;
}