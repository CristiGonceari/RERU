export interface EvaluationListModel {
    id?: number;
    evaluatedName: string;
    evaluatorName: string;
    counterSignerName: string;
    type: number;
    points: number;
    status: number;

    canEvaluate?: boolean;
    canCounterSign?: boolean;
    canDownload?: boolean;
    canDelete?: boolean;
    canAccept?: boolean;
    canAcceptCounterSign?: boolean;
}
