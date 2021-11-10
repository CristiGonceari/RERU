export class RulesModel {
    testTypeId: number;
    rules: string;

    constructor(rulesModel?: RulesModel) {
        if (rulesModel) {
            this.testTypeId = rulesModel.testTypeId;
            this.rules = rulesModel.rules;
        } else {
            this.testTypeId = null;
            this.rules = null;
        }
    }
}
