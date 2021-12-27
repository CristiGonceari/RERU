export class OptionModel {
    id?: number;
    questionUnitId: any;
    answer: string;
    isCorrect?: any;
    mediaFileId?: string;

    constructor(model?: OptionModel) {
        if (model) {
            this.id = model.id;
            this.questionUnitId = model.questionUnitId;
            this.answer = model.answer;
            this.isCorrect = model.isCorrect;
            this.mediaFileId = model.mediaFileId
        } else {
            this.id = null;
            this.questionUnitId = null;
            this.answer = null;
            this.isCorrect = null;
            this.mediaFileId = null;
        }
    }
}
