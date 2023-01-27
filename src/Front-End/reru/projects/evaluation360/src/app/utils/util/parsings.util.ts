import { FormGroup } from '@angular/forms';
import * as moment from 'moment';
import { Moment } from 'moment';
import { EvaluationModel, EvaluationAcceptModel, EvaluationCounterSignModel } from '../index';
import { EvaluationAcceptClass } from '../models/evaluation-accept.model';
import { EvaluationCounterSignClass } from '../models/evaluation-countersign.model';

export const parseEvaluation = (data: EvaluationModel): EvaluationModel => {
    return {
        id: data.id ? +data.id : undefined,
        subdivisionName: data.subdivisionName,
        evaluatedName: data.evaluatedName,
        // dateCompletionGeneralData: data.dateCompletionGeneralData,
        functionSubdivision: data.functionSubdivision,
        subdivisionEvaluated: data.subdivisionEvaluated,
        specialOrMilitaryGrade: +data.specialOrMilitaryGrade || null,
        periodEvaluatedFromTo: data.periodEvaluatedFromTo,
        periodEvaluatedUpTo: data.periodEvaluatedUpTo,
        educationEnum: +data.educationEnum || null,
        professionalTrainingActivities: +data.professionalTrainingActivities || null,
        professionalTrainingActivitiesType: +data?.professionalTrainingActivitiesType || null,
        courseName: data?.courseName,
        periodRunningActivityFromTo: data?.periodRunningActivityFromTo,
        periodRunningActivityUpTo: data?.periodRunningActivityUpTo,
        administrativeActOfStudies: data?.administrativeActOfStudies,
        serviceDuringEvaluationCourse: +data?.serviceDuringEvaluationCourse || null,
        functionEvaluated: data?.functionEvaluated,
        appointmentDate: data?.appointmentDate,
        administrativeActService: data?.administrativeActService,
        partialEvaluationPeriodFromTo: data?.partialEvaluationPeriodFromTo,
        partialEvaluationPeriodUpTo: data?.partialEvaluationPeriodUpTo,
        partialEvaluationScore: data?.partialEvaluationScore,
        qualifierPartialEvaluations: +data?.qualifierPartialEvaluations || null,
        sanctionAppliedEvaluationCourse: +data?.sanctionAppliedEvaluationCourse || null,
        dateSanctionApplication: data?.dateSanctionApplication,
        dateLiftingSanction: data?.dateLiftingSanction,
        qualificationEvaluationObtained2YearsPast: +data?.qualificationEvaluationObtained2YearsPast || null,
        qualificationEvaluationObtainedPreviousYear: +data?.qualificationEvaluationObtainedPreviousYear || null,
        qualificationQuarter1: +data?.qualificationQuarter1 || null,
        qualificationQuarter2: +data?.qualificationQuarter2 || null,
        qualificationQuarter3: +data?.qualificationQuarter3 || null,
        qualificationQuarter4: +data?.qualificationQuarter4 || null,
        question1: +data?.question1 || null,
        question2: +data?.question2 || null,
        question3: +data?.question3 || null,
        question4: +data?.question4 || null,
        question5: +data?.question5 || null,
        question6: +data?.question6 || null,
        question7: +data?.question7 || null,
        question8: +data?.question8 || null,
        question9: +data?.question9 || null,
        question10: +data?.question10 || null,
        question11: +data?.question11 || null,
        question12: +data?.question12 || null,
        question13: +data?.question13 || null,
        goal1: data.goal1,
        goal2: data.goal2,
        goal3: data.goal3,
        goal4: data.goal4,
        goal5: data.goal5,
        kpI1: data.kpI1,
        kpI2: data.kpI2,
        kpI3: data.kpI3,
        kpI4: data.kpI4,
        kpI5: data.kpI5,
        performanceTerm1: data.performanceTerm1,
        performanceTerm2: data.performanceTerm2,
        performanceTerm3: data.performanceTerm3,
        performanceTerm4: data.performanceTerm4,
        performanceTerm5: data.performanceTerm5,
        score1: +data.score1 || null,
        score2: +data.score2 || null,
        score3: +data.score3 || null,
        score4: +data.score4 || null,
        score5: +data.score5 || null,
        finalEvaluationQualification: +data.finalEvaluationQualification || null,
        dateEvaluationInterview: data.dateEvaluationInterview,
        dateSettingIindividualGoals: data.dateSettingIindividualGoals,
        need1ProfessionalDevelopmentEvaluated: data.need1ProfessionalDevelopmentEvaluated || null,
        need2ProfessionalDevelopmentEvaluated: data.need2ProfessionalDevelopmentEvaluated || null,
        need3ProfessionalDevelopmentEvaluated: data.need3ProfessionalDevelopmentEvaluated || null,
        need4ProfessionalDevelopmentEvaluated: data.need4ProfessionalDevelopmentEvaluated || null,
        need5ProfessionalDevelopmentEvaluated: data.need5ProfessionalDevelopmentEvaluated || null,
        commentsEvaluator: data.commentsEvaluator,
    }
}

export const parseEvaluatedModel = (data: EvaluationAcceptModel | EvaluationModel): EvaluationAcceptModel => {
    if (data instanceof EvaluationAcceptClass) {
        return {
            commentsEvaluated: data.commentsEvaluated
        }
    }

    return {
        commentsEvaluated: null,
    }
}

export const parseCounterSignModel = (data: EvaluationCounterSignModel | EvaluationModel): EvaluationCounterSignModel => {
    if (data instanceof EvaluationCounterSignClass) {
        return {
            checkComment1: !!data.checkComment1,
            checkComment2: !!data.checkComment2,
            checkComment3: !!data.checkComment3,
            checkComment4: !!data.checkComment4,
            otherComments: data.otherComments
        }
    }

    return {
        checkComment1: false,
        checkComment2: false,
        checkComment3: false,
        checkComment4: false,
        otherComments: null
    }
}

export const parseDate = (value: Moment | string, formControlName: string, form: FormGroup): void => {
    const nationalDateRegex = new RegExp(/^(0{1}[1-9]{1}|[1-2]{1}[0-9]{1}|[3]{1}[0-1])\.(0{1}[1-9]{1}|1{1}[0-2]{1})\.([1-9]{1}[0-9]{1}[0-9]{1}[0-9]{1})$/);
    if (typeof value === 'string' && value === '' && !form.get(formControlName)?.validator(form.get(formControlName))?.hasOwnProperty('required')) {
        form.get(formControlName).patchValue(null);
        form.get(formControlName).setErrors(null);
        form.get(formControlName).markAsTouched();
        form.get(formControlName).markAsDirty();
        return;
    }

    if (typeof value === 'string' && !nationalDateRegex.test(value as string)) {
        if (value) {
            form.get(formControlName).setErrors({ pattern: true });
        } else {
            form.get(formControlName).setErrors({ required: true });
        }
        form.get(formControlName).markAsTouched();
        form.get(formControlName).markAsDirty();
        return;
    }

    if (typeof value === 'string' && nationalDateRegex.test(value as string)) {
        const date = value.split('.');
        form.get(formControlName).patchValue(new Date(`${date[2]}-${date[1]}-${date[0]}`).toISOString());
        form.get(formControlName).markAsTouched();
        form.get(formControlName).markAsDirty();
    }

    if (typeof value === 'object') {
        const date = moment(value).toDate();
        form.get(formControlName).patchValue(new Date(date).toISOString());
        form.get(formControlName).markAsTouched();
        form.get(formControlName).markAsDirty();
    }
}

export async function generateAvgNumbers(requiredAvg) {
    let max = 4;
    let min = 1;
    let M1 = 0, M2 = 0, M3 = 0, M4 = 0;

    if (requiredAvg === 4) {
      max = 4;
      min = 3.50;
    }

    if (requiredAvg === 3) {
      max = 4;
      min = 2;
    }

    if (requiredAvg === 2) {
      max = 3;
      min = 1;
    }

    if (requiredAvg === 1) {
      max = 2;
      min = 1;
    }

    return new Promise((resolve, reject) => {
      let questions = [
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
  
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
  
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
  
        Math.floor(Math.random() * (max - min + 1)) + min,
      ]
  
      let scores = [
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
        Math.floor(Math.random() * (max - min + 1)) + min,
      ];
  
      let generateM1 = () => {
        return questions.slice(0,4).reduce((acc, curr) => acc + curr, 0) / 5;
      }
  
      let generateM2 = () => {
        return questions.slice(5,7).reduce((acc, curr) => acc + curr, 0) / 3;
      }
  
      let generateM3 = () => {
        return scores.reduce((acc, curr) => acc + curr, 0) / scores.length;
      }
  
      let generateM4 = () => {
        const pb = questions.slice(8,11).reduce((acc, curr) => acc + curr, 0) / 4;
        const pf = +questions[12];
  
        return (pb + pf) / 2;
      }
  
      let checkAvg = (M1, M2, M3, M4) => {
        const givenAvg = (M1 + M2 + M3 + M4) / 4;
        switch(requiredAvg) {
          case 1: 
            if (givenAvg >= 1 && givenAvg <= 1.50) {
              return true;
            } else {
              return false;
            }
          case 2: 
            if (givenAvg >= 1.51 && givenAvg <= 2.50) {
              return true;
            } else {
              return false;
            }
          case 3: 
            if (givenAvg >= 2.51 && givenAvg <= 3.50) {
              return true;
            } else {
              return false;
            }
          case 4: 
            if (givenAvg >= 3.51 && givenAvg <= 4) {
              return true;
            } else {
              return false;
            }
          default: return false;
        }
      }

      while(true) {
        M1 = generateM1();
        M2 = generateM2();
        M3 = generateM3();
        M4 = generateM4();

        if (checkAvg(M1, M2, M3, M4)) {
          break;
        }
  
        questions = [
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
  
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
  
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
  
          Math.floor(Math.random() * (max - min + 1)) + min,
        ]
    
        scores = [
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
          Math.floor(Math.random() * (max - min + 1)) + min,
        ];
      }
  
      if (requiredAvg === 4) {
        return resolve({
          questions: questions.map(el => Math.floor(el)),
          scores: scores.map(el => Math.floor(el))
        });
      }

      return resolve({
        questions,
        scores
      });
    })
  }
