export class RulesModel {
    testTemplateId: number;
    rules: string;

    constructor(rulesModel?: RulesModel) {
        if (rulesModel) {
            this.testTemplateId = rulesModel.testTemplateId;
            this.rules = rulesModel.rules;
        } else {
            this.testTemplateId = null;
            this.rules = null;
        }
    }
}
