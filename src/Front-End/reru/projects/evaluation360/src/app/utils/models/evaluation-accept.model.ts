export interface EvaluationAcceptModel {
    id?: number;
    commentsEvaluatedEmployee: string;
}

export interface EvaluationRejectModel extends EvaluationAcceptModel {}
