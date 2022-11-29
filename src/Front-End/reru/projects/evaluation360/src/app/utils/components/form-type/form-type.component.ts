import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Evaluation } from '../../models/evaluation.model';
import { EvaluationService } from '../../services/evaluations.service';

@Component({
  selector: 'app-form-type',
  templateUrl: './form-type.component.html',
  styleUrls: ['./form-type.component.scss']
})
export class FormTypeComponent implements OnInit {
  @Input() id: number;
  @Input() action: number;
  @Input() type: number;
  evaluation: Evaluation;
  constructor(private evaluationService: EvaluationService,
              private router: Router) { }

  ngOnInit(): void {
    this.getEvaluation(this.id);
  }

  getEvaluation(id: number): void {
    this.evaluation = {
      id: 1,
      evaluatedName: "string",
      subdivisionName: "string",
      dateCompletionGeneralData: "2022-11-29T21:51:25.298",
      nameSurnameEvaluatedEmployee: "string",
      functionSubdivision: "string",
      specialOrMilitaryGrade: "string",
      periodEvaluatedFromTo: "string",
      periodEvaluatedUpTo: "string",
      education: "string",
      professionalTrainingActivities: "string",
      courseName: "string",
      periodRunningActivity: "string",
      administrativeActOfStudies: "string",
      modificationServiceReportDuringEvaluationCourse: "string",
      function: "string",
      appointmentDate: "string",
      administrativeActService: "string",
      partialEvaluationPeriod: "string",
      finalScorePartialEvaluations: "string",
      qualifierPartialEvaluations: "string",
      sanctionAppliedEvaluationCourse: "string",
      dateSanctionApplication: "string",
      dateLiftingSanction: "string",
      qualificationEvaluationObtained2YearsPast: "string",
      qualificationEvaluationObtainedPreviousYear: "string",
      qualificationQuarter1: "string",
      qualificationQuarter2: "string",
      qualificationQuarter3: "string",
      qualificationQuarter4: "string",
      question1: "string",
      question2: "string",
      question3: "string",
      question4: "string",
      question5: "string",
      question6: "string",
      question7: "string",
      question8: "string",
      question9: "string",
      question10: "string",
      question11: "string",
      question12: "string",
      question13: "string",
      goal1: "string",
      goal2: "string",
      goal3: "string",
      goal4: "string",
      goal5: "string",
      kpI1: "string",
      kpI2: "string",
      kpI3: "string",
      kpI4: "string",
      kpI5: "string",
      performanceTerm1: "string",
      performanceTerm2: "string",
      performanceTerm3: "string",
      performanceTerm4: "string",
      performanceTerm5: "string",
      score1: "string",
      score2: "string",
      score3: "string",
      score4: "string",
      score5: "string",
      finalEvaluationQualification: "string",
      dateEvaluatiorInterview: "string",
      dateSettingIindividualGoals: "string",
      need1ProfessionalDevelopmentEvaluatedEmployee: "string",
      need2ProfessionalDevelopmentEvaluatedEmployee: "string",
      evaluatorComments: "string"
    };
    this.type = 1;
    // this.evaluationService.get(id).subscribe(response => {
		// 	if (!response) {
		// 		this.router.navigate(['/']);
		// 		return;
    //   }
    //   this.type = response.type;
		// 	this.evaluation = response;
		// });
  }
}
