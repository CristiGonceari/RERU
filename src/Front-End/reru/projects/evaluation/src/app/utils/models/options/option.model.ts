export class OptionModel {
    id?: number;
    questionUnitId: number;
    answer: string;
    isCorrect?: boolean;

    constructor(model?: OptionModel) {
        if (model) {
            this.id = model.id;
            this.questionUnitId = model.questionUnitId;
            this.answer = model.answer;
            this.isCorrect = model.isCorrect;
        } else {
            this.id = null;
            this.questionUnitId = null;
            this.answer = null;
            this.isCorrect = null;
        }
    }
}
