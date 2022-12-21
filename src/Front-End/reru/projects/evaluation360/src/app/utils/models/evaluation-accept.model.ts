import { EvaluationModel } from "./evaluation.model";

export interface EvaluationAcceptModel {
    commentsEvaluated: string;
}

export interface EvaluationRejectModel extends EvaluationAcceptModel {}

export class EvaluationAcceptClass implements EvaluationAcceptModel {
    commentsEvaluated: string;
    constructor(data?: EvaluationAcceptModel | EvaluationModel ) {
        if (data) {
            this.commentsEvaluated = data.commentsEvaluated;
        } else {
            this.commentsEvaluated = null;
        }
    }
}
