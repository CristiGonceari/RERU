export interface EvaluationCounterSignModel {
    id: number;
    checkComment1: boolean;
    checkComment2: boolean;
    checkComment3: boolean;
    checkComment4: boolean;
    otherComments: string;
}

export class EvaluationCounterSignClass implements EvaluationCounterSignModel {
    id: number;
    checkComment1: boolean;
    checkComment2: boolean;
    checkComment3: boolean;
    checkComment4: boolean;
    otherComments: string;
    constructor(data?: EvaluationCounterSignModel) {
        if (data) {
            this.id = data.id;
            this.checkComment1 = data.checkComment1;
            this.checkComment2 = data.checkComment2;
            this.checkComment3 = data.checkComment3;
            this.checkComment4 = data.checkComment4;
            this.otherComments = data.otherComments;
        } else {
            this.checkComment1 = null;
            this.checkComment2 = null;
            this.checkComment3 = null;
            this.checkComment4 = null;
            this.otherComments = null;
        }
    }
}
