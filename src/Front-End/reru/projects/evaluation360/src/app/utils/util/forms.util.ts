import { AbstractControl, FormBuilder, FormGroup, Validators } from "@angular/forms"
import { EvaluationAcceptModel } from "../models/evaluation-accept.model"
import { EvaluationCounterSignModel } from "../models/evaluation-countersign.model"
import { EvaluationRoleEnum } from "../models/evaluation-role.enum"
import { EvaluationModel } from "../models/evaluation.model"

const fb = new FormBuilder();

export const createEvaluatorForm = (data: EvaluationModel, evaluationRoleEnum: EvaluationRoleEnum = EvaluationRoleEnum.Evaluator) => {
    return fb.group({
      id: fb.control({value: data?.id, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      subdivisionName: fb.control({value: data?.subdivisionName, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.required, Validators.pattern(/^[A-Za-z0-9- ]+$/)]),
      evaluatedName: fb.control({value: data?.evaluatedName, disabled: true }, []),
      dateCompletionGeneralData: fb.control({value: data?.dateCompletionGeneralData, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.required, Validators.pattern(/^\d{1,2}\.\d{1,2}\.\d{4}$/)]),
      nameSurnameEvaluated: fb.control({value: data?.nameSurnameEvaluated, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.required, Validators.pattern(/[A-Za-z- ]*/)]),
      functionSubdivision: fb.control({value: data?.functionSubdivision, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.required, Validators.pattern(/[A-Za-z- ]*/)]),
      subdivisionEvaluated: fb.control({value: data?.subdivisionEvaluated, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.required, Validators.pattern(/[A-Za-z- ]*/)]),
      specialOrMilitaryGrade: fb.control({value: data?.specialOrMilitaryGrade, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.required]),
      periodEvaluatedFromTo: fb.control({value: data?.periodEvaluatedFromTo, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.required, Validators.pattern(/^\d{1,2}\.\d{1,2}\.\d{4}$/)]),
      periodEvaluatedUpTo: fb.control({value: data?.periodEvaluatedUpTo, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.required, Validators.pattern(/^\d{1,2}\.\d{1,2}\.\d{4}$/)]),
      educationEnum: fb.control({value: data?.educationEnum, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      professionalTrainingActivities: fb.control({value: data?.professionalTrainingActivities, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      professionalTrainingActivitiesType: fb.control({value: data?.professionalTrainingActivitiesType, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      courseName: fb.control({value: data?.courseName, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      periodRunningActivityFromTo: fb.control({value: data?.periodRunningActivityFromTo, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.pattern(/^\d{1,2}\.\d{1,2}\.\d{4}$/)]),
      periodRunningActivityUpTo: fb.control({value: data?.periodRunningActivityUpTo, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, [Validators.pattern(/^\d{1,2}\.\d{1,2}\.\d{4}$/)]),
      administrativeActOfStudies: fb.control({value: data?.administrativeActOfStudies, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      serviceDuringEvaluationCourse: fb.control({value: data?.serviceDuringEvaluationCourse, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}),
      functionEvaluated: fb.control({value: data?.functionEvaluated, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      appointmentDate: fb.control({value: data?.appointmentDate, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      administrativeActService: fb.control({value: data?.administrativeActService, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      partialEvaluationPeriodFromTo: fb.control({value: data?.partialEvaluationPeriodFromTo, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      partialEvaluationPeriodUpTo: fb.control({value: data?.partialEvaluationPeriodUpTo, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      partialEvaluationScore: fb.control({value: data?.partialEvaluationScore, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      qualifierPartialEvaluations: fb.control({value: data?.qualifierPartialEvaluations, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      sanctionAppliedEvaluationCourse: fb.control({value: data?.sanctionAppliedEvaluationCourse, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      dateSanctionApplication: fb.control({value: data?.dateSanctionApplication, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      dateLiftingSanction: fb.control({value: data?.dateLiftingSanction, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      qualificationEvaluationObtained2YearsPast: fb.control({value: data?.qualificationEvaluationObtained2YearsPast, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      qualificationEvaluationObtainedPreviousYear: fb.control({value: data?.qualificationEvaluationObtainedPreviousYear, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      qualificationQuarter1: fb.control({value: data?.qualificationQuarter1, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      qualificationQuarter2: fb.control({value: data?.qualificationQuarter2, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      qualificationQuarter3: fb.control({value: data?.qualificationQuarter3, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      qualificationQuarter4: fb.control({value: data?.qualificationQuarter4, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question1: fb.control({value: data?.question1, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question2: fb.control({value: data?.question2, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question3: fb.control({value: data?.question3, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question4: fb.control({value: data?.question4, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question5: fb.control({value: data?.question5, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question6: fb.control({value: data?.question6, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question7: fb.control({value: data?.question7, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question8: fb.control({value: data?.question8, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question9: fb.control({value: data?.question9, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question10: fb.control({value: data?.question10, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question11: fb.control({value: data?.question11, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question12: fb.control({value: data?.question12, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      question13: fb.control({value: data?.question13, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      goal1: fb.control({value: data?.goal1, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      goal2: fb.control({value: data?.goal2, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      goal3: fb.control({value: data?.goal3, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      goal4: fb.control({value: data?.goal4, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      goal5: fb.control({value: data?.goal5, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      kpI1: fb.control({value: data?.kpI1, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      kpI2: fb.control({value: data?.kpI2, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      kpI3: fb.control({value: data?.kpI3, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      kpI4: fb.control({value: data?.kpI4, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      kpI5: fb.control({value: data?.kpI5, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      performanceTerm1: fb.control({value: data?.performanceTerm1, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      performanceTerm2: fb.control({value: data?.performanceTerm2, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      performanceTerm3: fb.control({value: data?.performanceTerm3, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      performanceTerm4: fb.control({value: data?.performanceTerm4, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      performanceTerm5: fb.control({value: data?.performanceTerm5, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      score1: fb.control({value: data?.score1, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      score2: fb.control({value: data?.score2, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      score3: fb.control({value: data?.score3, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      score4: fb.control({value: data?.score4, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      score5: fb.control({value: data?.score5, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      finalEvaluationQualification: fb.control({value: data?.finalEvaluationQualification, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      dateEvaluatiorInterview: fb.control({value: data?.dateEvaluatiorInterview, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      dateSettingIindividualGoals: fb.control({value: data?.dateSettingIindividualGoals, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      need1ProfessionalDevelopmentEvaluated: fb.control({value: data?.need1ProfessionalDevelopmentEvaluated, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      need2ProfessionalDevelopmentEvaluated: fb.control({value: data?.need2ProfessionalDevelopmentEvaluated, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
      commentsEvaluator: fb.control({value: data?.commentsEvaluator, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluator}, []),
    })
}

export const createEvaluatedForm = (data: EvaluationAcceptModel, evaluationRoleEnum: EvaluationRoleEnum = EvaluationRoleEnum.Evaluated) => {
    return fb.group({
        id: fb.control(data.id),
        commentsEvaluated: fb.control({value: data.commentsEvaluated, disabled: evaluationRoleEnum != EvaluationRoleEnum.Evaluated })
    })
}

export const createCounterSignForm = (data: EvaluationCounterSignModel, evaluationRoleEnum: EvaluationRoleEnum = EvaluationRoleEnum.CounterSigner) => {
    return fb.group({
        id: fb.control(data.id),
        checkComment1: fb.control({value: data.checkComment1, disabled: evaluationRoleEnum != EvaluationRoleEnum.CounterSigner }),
        checkComment2: fb.control({value: data.checkComment2, disabled: evaluationRoleEnum != EvaluationRoleEnum.CounterSigner }),
        checkComment3: fb.control({value: data.checkComment3, disabled: evaluationRoleEnum != EvaluationRoleEnum.CounterSigner }),
        checkComment4: fb.control({value: data.checkComment4, disabled: evaluationRoleEnum != EvaluationRoleEnum.CounterSigner }),
        otherComments: fb.control({value: data.otherComments, disabled: evaluationRoleEnum != EvaluationRoleEnum.CounterSigner })
    })
}


export const isValid = (form: FormGroup, field: string): boolean => {
    const f: AbstractControl = form.get(field);
    if (f.dirty || f.touched) {
        return f.valid;
    }

    return false;
}

export const isInvalidRequired = (form: FormGroup, field: string): boolean => {
    const f: AbstractControl = form.get(field);
    if (f && f.invalid && (f.dirty || f.touched)) {
        return f.errors?.required;
    }

    return false;
}

export const isInvalidPattern = (form: FormGroup, field: string): boolean => {
    console.log('field', field);
    const f: AbstractControl = form.get(field);
    console.log('f.invalid = ', f.invalid)
    console.log('f.dirty = ', f.dirty)
    console.log('f.touched = ', f.touched)
    if (f.dirty || f.touched) {
        console.log('f.errors', f.errors)
        return f.errors?.pattern;
    }

    return false;
}

export const isInvalidMinLength = (form: FormGroup, field: string): boolean => {
    const f: AbstractControl = form.get(field);
    if (f && f.invalid && (f.dirty || f.touched)) {
        return f.errors?.minlength;
    }

    return false;
}

export const isInvalidMaxLength = (form: FormGroup, field: string): boolean => {
    const f: AbstractControl = form.get(field);
    if (f && f.invalid && (f.dirty || f.touched)) {
        return f.errors?.maxlength;
    }

    return false;
}

export const isInvalidMin = (form: FormGroup, field: string): boolean => {
    const f: AbstractControl = form.get(field);
    if (f && f.invalid && (f.dirty || f.touched)) {
        return f.errors?.min;
    }

    return false;
}

export const isInvalidMax = (form: FormGroup, field: string): boolean => {
    const f: AbstractControl = form.get(field);
    if (f && f.invalid && (f.dirty || f.touched)) {
        return f.errors?.max;
    }

    return false;
}

export const isInvalidCustom = (form: FormGroup, field: string, custom: string): boolean => {
    const f: AbstractControl = form.get(field);
    if (f && f.invalid && (f.dirty || f.touched)) {
        return f.errors?.[custom];
    }

    return false;
}
