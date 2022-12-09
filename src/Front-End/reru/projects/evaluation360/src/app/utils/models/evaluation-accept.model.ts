export interface EvaluationAcceptModel {
    id: number;
    commentsEvaluated: string;
}

export interface EvaluationRejectModel extends EvaluationAcceptModel {}

export class EvaluationAcceptClass implements EvaluationAcceptModel {
    id: number;
    commentsEvaluated: string;
}
