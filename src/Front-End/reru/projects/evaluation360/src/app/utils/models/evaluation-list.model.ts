export interface EvaluationListModel {
    id?: number;
    evaluatedName: string;
    evaluatorName: string;
    counterSignerName: string;
    type: number;
    points: number;
    status: number;

    canEvaluate?: boolean;
    canAccept?: boolean;
    canCounterSign?: boolean;
    canDownload?: boolean;
    canDelete?: boolean;
    canFinished?: boolean;
}

export interface EvaluationListQueries {
    evaluatedName?: string;
    evaluatorName?: string;
    counterSignerName?: string;
    type?: string;
    status?: string;
    createDateFrom?: string | Date;
    createDateTo?: string | Date;
}
