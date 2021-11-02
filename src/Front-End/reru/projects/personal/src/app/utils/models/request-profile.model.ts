export interface RequestProfileModel {
    id?: number;
    from: string;
    status: number;
    requestId: number;
    requestName: string;
    orderId: number;
    orderName: string;
    positionId: number;
    positionOrganizationRoleName: string;
    contractorName?: string;
    contractorLastName?: string;
    contractorId: number;
}
