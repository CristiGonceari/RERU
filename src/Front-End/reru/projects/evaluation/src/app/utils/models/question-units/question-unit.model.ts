import { QuestionUnitStatusEnum } from "../../enums/question-unit-status.enum";
import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";

export class QuestionUnit {
        id: number;
        status: QuestionUnitStatusEnum;
        questionType: QuestionUnitTypeEnum;
        answersCount: number;
        usedInTestsCount: number;
        question: string;
        categoryName: string;
        questionCategoryId: number;
        tags: string[];
        questionPoints: number;    
        
        constructor(questionUnitModel?: QuestionUnit) {
                if (questionUnitModel) {
                    this.id = questionUnitModel.id;
                    this.questionCategoryId = questionUnitModel.questionCategoryId;
                    this.question = questionUnitModel.question;
                    this.questionType = questionUnitModel.questionType;
                    this.status = questionUnitModel.status;
                } else {
                    this.id = null;
                    this.questionCategoryId = null;
                    this.question = null;
                    this.questionType = null;
                    this.status = null;
                }
            }
}

