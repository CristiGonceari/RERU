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
        specialOrMilitaryGrade: +data.specialOrMilitaryGrade,
        periodEvaluatedFromTo: data.periodEvaluatedFromTo,
        periodEvaluatedUpTo: data.periodEvaluatedUpTo,
        educationEnum: +data.educationEnum,
        professionalTrainingActivities: +data.professionalTrainingActivities,
        professionalTrainingActivitiesType: +data?.professionalTrainingActivitiesType,
        courseName: data?.courseName,
        periodRunningActivityFromTo: data?.periodRunningActivityFromTo,
        periodRunningActivityUpTo: data?.periodRunningActivityUpTo,
        administrativeActOfStudies: data?.administrativeActOfStudies,
        serviceDuringEvaluationCourse: +data?.serviceDuringEvaluationCourse,
        functionEvaluated: data?.functionEvaluated,
        appointmentDate: data?.appointmentDate,
        administrativeActService: data?.administrativeActService,
        partialEvaluationPeriodFromTo: data?.partialEvaluationPeriodFromTo,
        partialEvaluationPeriodUpTo: data?.partialEvaluationPeriodUpTo,
        partialEvaluationScore: data?.partialEvaluationScore,
        qualifierPartialEvaluations: +data?.qualifierPartialEvaluations,
        sanctionAppliedEvaluationCourse: data?.sanctionAppliedEvaluationCourse,
        dateSanctionApplication: data?.dateSanctionApplication,
        dateLiftingSanction: data?.dateLiftingSanction,
        qualificationEvaluationObtained2YearsPast: +data?.qualificationEvaluationObtained2YearsPast,
        qualificationEvaluationObtainedPreviousYear: +data?.qualificationEvaluationObtainedPreviousYear,
        qualificationQuarter1: +data?.qualificationQuarter1,
        qualificationQuarter2: +data?.qualificationQuarter2,
        qualificationQuarter3: +data?.qualificationQuarter3,
        qualificationQuarter4: +data?.qualificationQuarter4,
        question1: +data?.question1,
        question2: +data?.question2,
        question3: +data?.question3,
        question4: +data?.question4,
        question5: +data?.question5,
        question6: +data?.question6,
        question7: +data?.question7,
        question8: +data?.question8,
        question9: +data?.question9,
        question10: +data?.question10,
        question11: +data?.question11,
        question12: +data?.question12,
        question13: +data?.question13,
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
        score1: +data.score1,
        score2: +data.score2,
        score3: +data.score3,
        score4: +data.score4,
        score5: +data.score5,
        finalEvaluationQualification: +data.finalEvaluationQualification,
        dateEvaluationInterview: data.dateEvaluationInterview,
        dateSettingIindividualGoals: data.dateSettingIindividualGoals,
        need1ProfessionalDevelopmentEvaluated: data.need1ProfessionalDevelopmentEvaluated,
        need2ProfessionalDevelopmentEvaluated: data.need2ProfessionalDevelopmentEvaluated,
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
    if (typeof value === 'string' && value === '' && !form?.get(formControlName)?.errors?.required) {
        form.get(formControlName).setErrors(null);
        form.get(formControlName).markAsTouched();
        form.get(formControlName).markAsDirty();
        return;
    }

    if (typeof value === 'string' && !nationalDateRegex.test(value as string)) {
        form.get(formControlName).setErrors({ pattern: true });
        form.get(formControlName).markAsTouched();
        return;
    }

    if (typeof value === 'string') {
        const date = '12.12.2020'.split('.');
        form.get(formControlName).patchValue(new Date(`${date[2]}-${date[1]}-${date[0]}`).toISOString());
        form.get(formControlName).markAsTouched();
    }

    if (typeof value === 'object') {
        const date = moment(value).toDate();
        form.get(formControlName).patchValue(new Date(date).toISOString());
        form.get(formControlName).markAsTouched();
    }
}
