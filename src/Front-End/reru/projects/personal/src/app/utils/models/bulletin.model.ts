export interface BulletinModel {
    id?: number;
    series: string;
    releaseDay: string;
    emittedBy: string;
    idnp: string;
    contractorId?: number;
    birthPlace: AddressModel;
    livingAddress: AddressModel;
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
}
