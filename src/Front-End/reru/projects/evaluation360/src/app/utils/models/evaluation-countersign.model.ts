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
}
