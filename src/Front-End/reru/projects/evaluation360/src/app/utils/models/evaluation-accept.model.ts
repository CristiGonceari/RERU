export interface EvaluationAcceptModel {
    id: number;
    commentsEvaluated: string;
}

export interface EvaluationRejectModel extends EvaluationAcceptModel {}

export class EvaluationAcceptClass implements EvaluationAcceptModel {
    id: number;
    commentsEvaluated: string;
    constructor(data?: EvaluationAcceptModel) {
        if (data) {
            this.id = data.id;
            this.commentsEvaluated = data.commentsEvaluated;
        } else {
            this.commentsEvaluated = null;
        }
    }
}
