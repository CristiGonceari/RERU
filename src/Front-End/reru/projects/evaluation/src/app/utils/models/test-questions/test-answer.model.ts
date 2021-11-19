export class TestAnswer {
    optionId: number;
    answerValue: string;

    constructor(testAnswerModel?: TestAnswer) {
        if (testAnswerModel) {
            this.optionId = testAnswerModel.optionId;
            this.answerValue = testAnswerModel.answerValue;
        } else {
            this.optionId = null;
            this.answerValue = null;
        }
    }
}
