import { Component, OnInit } from '@angular/core';
import { StatisticsQuestionFilterEnum } from '../../../utils/enums/statistics-question-filter.enum';
import { StatisticService } from '../../../utils/services/statistic/statistic.service';

@Component({
  selector: 'app-statisctics-table',
  templateUrl: './statisctics-table.component.html',
  styleUrls: ['./statisctics-table.component.scss']
})
export class StatiscticsTableComponent implements OnInit {

  questionList = [];
  data;
  enum = StatisticsQuestionFilterEnum;

  constructor(
    private statisticService: StatisticService
    ) { }

  ngOnInit(): void {
  }

  getQuestions(data) {
    this.data = data;
    if (data.categoryId)
      this.statisticService.getCategoryQuestions(data).subscribe((res) => { this.questionList = res.data });
    else if (data.testTypeId)
      this.statisticService.getTestTypeQuestions(data).subscribe((res) => { this.questionList = res.data });
  }
}
