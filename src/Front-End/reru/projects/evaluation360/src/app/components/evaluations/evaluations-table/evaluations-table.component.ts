import { Component, OnInit } from '@angular/core';
import { EvaluationService } from '../../../utils/services/survey.service';

@Component({
  selector: 'app-evaluations-table',
  templateUrl: './evaluations-table.component.html',
  styleUrls: ['./evaluations-table.component.scss']
})
export class EvaluationsTableComponent implements OnInit {
  isLoading: boolean = true;
  surveys: any[] = [];
  pagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  constructor(private readonly evaluationService: EvaluationService) { }

  ngOnInit(): void {
    this.list();
  }

  list(data: any = {}): void {
    this.isLoading = true;
    const request = {
      ...data,
      page: data.page || this.pagedSummary.currentPage
    }
    this.evaluationService.listMine(request).subscribe(response => {
      this.surveys = response;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

}
