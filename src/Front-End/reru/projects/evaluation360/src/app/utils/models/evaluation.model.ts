export interface EvaluationModel {
	id?: number;
	subdivisionName: string;
	evaluatedName?: string;
	// dateCompletionGeneralData: string | Date;
	functionSubdivision: string;
	specialOrMilitaryGrade: number;
	periodEvaluatedFromTo: string;
	periodEvaluatedUpTo: string;
	educationEnum: number;
	professionalTrainingActivities: number;
	professionalTrainingActivitiesType: number;
	courseName: string;
	periodRunningActivityFromTo: string;
	periodRunningActivityUpTo: string;
	administrativeActOfStudies: string;
	serviceDuringEvaluationCourse: number;
	functionEvaluated: string;
	subdivisionEvaluated: string;
	appointmentDate: string | Date;
	administrativeActService: string;
	partialEvaluationPeriodFromTo: string | Date;
	partialEvaluationPeriodUpTo: string | Date;
	partialEvaluationScore: number;
	qualifierPartialEvaluations: number;
	sanctionAppliedEvaluationCourse: string;
	dateSanctionApplication: string | Date;
	dateLiftingSanction: string | Date;
	qualificationEvaluationObtained2YearsPast: number;
	qualificationEvaluationObtainedPreviousYear: number;
	qualificationQuarter1: number;
	qualificationQuarter2: number;
	qualificationQuarter3: number;
	qualificationQuarter4: number;
	question1: number;
	question2: number;
	question3: number;
	question4: number;
	question5: number;
	question6: number;
	question7: number;
	question8: number;
	question9: number;
	question10: number;
	question11: number;
	question12: number;
	question13: number;
	goal1: string;
	goal2: string;
	goal3: string;
	goal4: string;
	goal5: string;
	kpI1: string;
	kpI2: string;
	kpI3: string;
	kpI4: string;
	kpI5: string;
	performanceTerm1: string;
	performanceTerm2: string;
	performanceTerm3: string;
	performanceTerm4: string;
	performanceTerm5: string;
	score1: number;
	score2: number;
	score3: number;
	score4: number;
	score5: number;
	finalEvaluationQualification: number;
	dateEvaluationInterview: string | Date;
	dateSettingIindividualGoals: string | Date;
	need1ProfessionalDevelopmentEvaluated: string;
	need2ProfessionalDevelopmentEvaluated: string;
	need3ProfessionalDevelopmentEvaluated: string;
	need4ProfessionalDevelopmentEvaluated: string;
	need5ProfessionalDevelopmentEvaluated: string;
	commentsEvaluator: string;

	/* Read-onlyy mode */
	type?: number;
	evaluatorName?: string;
	commentsEvaluated?: string;
	functionEvaluator?: string;
	checkComment1?: boolean;
	checkComment2?: boolean;
	checkComment3?: boolean;
	checkComment4?: boolean;
	otherComments?: string;
	counterSignerName?: string;
	functionCounterSigner?: string;
}

export class EvaluationClass implements EvaluationModel {
	id?: number;
	subdivisionName: string;
	evaluatedName: string;
	// dateCompletionGeneralData: string | Date;
	functionSubdivision: string;
	specialOrMilitaryGrade: number;
	periodEvaluatedFromTo: string;
	periodEvaluatedUpTo: string;
	educationEnum: number;
	professionalTrainingActivities: number;
	professionalTrainingActivitiesType: number;
	courseName: string;
	periodRunningActivityFromTo: string;
	periodRunningActivityUpTo: string;
	administrativeActOfStudies: string;
	serviceDuringEvaluationCourse: number;
	functionEvaluated: string;
	subdivisionEvaluated: string;
	appointmentDate: string | Date;
	administrativeActService: string;
	partialEvaluationPeriodFromTo: string | Date;
	partialEvaluationPeriodUpTo: string | Date;
	partialEvaluationScore: number;
	qualifierPartialEvaluations: number;
	sanctionAppliedEvaluationCourse: string;
	dateSanctionApplication: string | Date;
	dateLiftingSanction: string | Date;
	qualificationEvaluationObtained2YearsPast: number;
	qualificationEvaluationObtainedPreviousYear: number;
	qualificationQuarter1: number;
	qualificationQuarter2: number;
	qualificationQuarter3: number;
	qualificationQuarter4: number;
	question1: number;
	question2: number;
	question3: number;
	question4: number;
	question5: number;
	question6: number;
	question7: number;
	question8: number;
	question9: number;
	question10: number;
	question11: number;
	question12: number;
	question13: number;
	goal1: string;
	goal2: string;
	goal3: string;
	goal4: string;
	goal5: string;
	kpI1: string;
	kpI2: string;
	kpI3: string;
	kpI4: string;
	kpI5: string;
	performanceTerm1: string;
	performanceTerm2: string;
	performanceTerm3: string;
	performanceTerm4: string;
	performanceTerm5: string;
	score1: number;
	score2: number;
	score3: number;
	score4: number;
	score5: number;
	finalEvaluationQualification: number;
	dateEvaluationInterview: string | Date;
	dateSettingIindividualGoals: string | Date;
	need1ProfessionalDevelopmentEvaluated: string;
	need2ProfessionalDevelopmentEvaluated: string;
	need3ProfessionalDevelopmentEvaluated: string;
	need4ProfessionalDevelopmentEvaluated: string;
	need5ProfessionalDevelopmentEvaluated: string;
	commentsEvaluator: string;
	constructor(evaluation?: EvaluationModel) {
		if (evaluation) {
			this.id = evaluation.id;
			this.subdivisionName = evaluation.subdivisionName;
			this.evaluatedName = evaluation.evaluatedName;
			// this.dateCompletionGeneralData = evaluation.dateCompletionGeneralData;
			this.functionSubdivision = evaluation.functionSubdivision;
			this.subdivisionEvaluated = evaluation.subdivisionEvaluated;
			this.specialOrMilitaryGrade = +evaluation.specialOrMilitaryGrade;
			this.periodEvaluatedFromTo = evaluation.periodEvaluatedFromTo;
			this.periodEvaluatedUpTo = evaluation.periodEvaluatedUpTo;
			this.educationEnum = +evaluation.educationEnum;
			this.professionalTrainingActivities = +evaluation.professionalTrainingActivities;
			this.professionalTrainingActivitiesType = +evaluation.professionalTrainingActivitiesType;
			this.courseName = evaluation.courseName;
			this.periodRunningActivityFromTo = evaluation.periodRunningActivityFromTo;
			this.periodRunningActivityUpTo = evaluation.periodRunningActivityUpTo;
			this.administrativeActOfStudies = evaluation.administrativeActOfStudies;
			this.serviceDuringEvaluationCourse = +evaluation.serviceDuringEvaluationCourse;
			this.functionEvaluated = evaluation.functionEvaluated;
			this.appointmentDate = evaluation.appointmentDate;
			this.administrativeActService = evaluation.administrativeActService;
			this.partialEvaluationPeriodFromTo = evaluation.partialEvaluationPeriodFromTo;
			this.partialEvaluationPeriodUpTo = evaluation.partialEvaluationPeriodUpTo;
			this.partialEvaluationScore = evaluation.partialEvaluationScore || null;
			this.qualifierPartialEvaluations = +evaluation.qualifierPartialEvaluations;
			this.sanctionAppliedEvaluationCourse = evaluation.sanctionAppliedEvaluationCourse;
			this.dateSanctionApplication = evaluation.dateSanctionApplication;
			this.dateLiftingSanction = evaluation.dateLiftingSanction;
			this.qualificationEvaluationObtained2YearsPast = +evaluation.qualificationEvaluationObtained2YearsPast;
			this.qualificationEvaluationObtainedPreviousYear = +evaluation.qualificationEvaluationObtainedPreviousYear;
			this.qualificationQuarter1 = +evaluation.qualificationQuarter1 || null;
			this.qualificationQuarter2 = +evaluation.qualificationQuarter2 || null;
			this.qualificationQuarter3 = +evaluation.qualificationQuarter3 || null;
			this.qualificationQuarter4 = +evaluation.qualificationQuarter4 || null;
			this.question1 = +evaluation.question1 || null;
			this.question2 = +evaluation.question2 || null;
			this.question3 = +evaluation.question3 || null;
			this.question4 = +evaluation.question4 || null;
			this.question5 = +evaluation.question5 || null;
			this.question6 = +evaluation.question6 || null;
			this.question7 = +evaluation.question7 || null;
			this.question8 = +evaluation.question8 || null;
			this.question9 = +evaluation.question9 || null;
			this.question10 = +evaluation.question10 || null;
			this.question11 = +evaluation.question11 || null;
			this.question12 = +evaluation.question12 || null;
			this.question13 = +evaluation.question13 || null;
			this.goal1 = evaluation.goal1;
			this.goal2 = evaluation.goal2;
			this.goal3 = evaluation.goal3;
			this.goal4 = evaluation.goal4;
			this.goal5 = evaluation.goal5;
			this.kpI1 = evaluation.kpI1;
			this.kpI2 = evaluation.kpI2;
			this.kpI3 = evaluation.kpI3;
			this.kpI4 = evaluation.kpI4;
			this.kpI5 = evaluation.kpI5;
			this.performanceTerm1 = evaluation.performanceTerm1;
			this.performanceTerm2 = evaluation.performanceTerm2;
			this.performanceTerm3 = evaluation.performanceTerm3;
			this.performanceTerm4 = evaluation.performanceTerm4;
			this.performanceTerm5 = evaluation.performanceTerm5;
			this.score1 = +evaluation.score1 || null;
			this.score2 = +evaluation.score2 || null;
			this.score3 = +evaluation.score3 || null;
			this.score4 = +evaluation.score4 || null;
			this.score5 = +evaluation.score5 || null;
			this.finalEvaluationQualification = +evaluation.finalEvaluationQualification || null;
			this.dateEvaluationInterview = evaluation.dateEvaluationInterview;
			this.dateSettingIindividualGoals = evaluation.dateSettingIindividualGoals;
			this.need1ProfessionalDevelopmentEvaluated = evaluation.need1ProfessionalDevelopmentEvaluated;
			this.need2ProfessionalDevelopmentEvaluated = evaluation.need2ProfessionalDevelopmentEvaluated;
			this.need3ProfessionalDevelopmentEvaluated = evaluation.need3ProfessionalDevelopmentEvaluated;
			this.need4ProfessionalDevelopmentEvaluated = evaluation.need4ProfessionalDevelopmentEvaluated;
			this.need5ProfessionalDevelopmentEvaluated = evaluation.need5ProfessionalDevelopmentEvaluated;
			this.commentsEvaluator = evaluation.commentsEvaluator;
		}
	}
}
