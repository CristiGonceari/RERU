export class MilitaryObligationModel {
    id?: number;
    militaryObligationType: string;
    mobilizationYear: Date;
    withdrawalYear: Date;
    efectiv : string;
    militarySpecialty : string;
    degree : string;
    institutionName: string;
    institutionAdress: string;
    militaryBookletSeries : string;
    militaryBookletNumber : number;
    militaryBookletReleaseDay : Date;
    militaryBookletEminentAuthority : string;
    startObligationPeriod : Date;
    endObligationPeriod : Date;
    contractorId: number;
}
