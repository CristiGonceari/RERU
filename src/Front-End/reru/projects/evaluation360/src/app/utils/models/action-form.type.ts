import { EvaluationAcceptModel } from "./evaluation-accept.model";
import { EvaluationCounterSignModel } from "./evaluation-countersign.model";
import { EvaluationModel } from "./evaluation.model";

export type ActionFormType = 'isSave' | 
                             'isConfirm'| 
                             'isAccept' | 
                             'isReject' | 
                             'isCounterSignAccept' |
                             'isCounterSignReject' |
                             'isAcknowledge';

export enum ActionFormEnum {
    isSave = 'isSave',
    isConfirm = 'isConfirm',
    isAccept = 'isAccept',
    isReject = 'isReject',
    isCounterSignAccept = 'isCounterSignAccept',
    isCounterSignReject = 'isCounterSignReject',
    isAcknowledge = 'isAcknowledge'
}

export interface ActionFormModel {
    action: ActionFormType;
    data: EvaluationModel | EvaluationAcceptModel | EvaluationCounterSignModel;
}
