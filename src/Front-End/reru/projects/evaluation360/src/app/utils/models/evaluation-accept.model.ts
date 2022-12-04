export interface EvaluationAcceptModel {
    id: number;
    commentsEvaluated: string;
    dateAcceptOrRejectEvaluated: string | Date;
    signatureEvaluated: boolean;
}

export interface EvaluationRejectModel extends EvaluationAcceptModel {}
