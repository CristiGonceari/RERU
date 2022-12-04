export interface EvaluationCounterSignModel {
 id: number;
 checkComment1: boolean;
 checkComment2: boolean;
 checkComment3: boolean;
 checkComment4: boolean;
 otherComments: string;
 functionCounterSigner: string;
 dateCompletionCounterSigner: string | Date;
 signatureCounterSigner: boolean;
}
