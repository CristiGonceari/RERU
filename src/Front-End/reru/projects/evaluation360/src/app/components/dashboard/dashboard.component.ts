import { Component } from '@angular/core';
import { I18nService } from '../../utils/services/i18n.service';
import { forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';
import { EvaluationService } from '../../utils/services/evaluations.service';
import {
  ApexChart,
  ApexAxisChartSeries,
  ApexXAxis,
  ApexStroke,
  ApexTooltip,
  ApexDataLabels,
  ApexYAxis
} from "ng-apexcharts";

export type TodaysEvaluations = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis | ApexYAxis[];
  stroke: ApexStroke;
  tooltip: ApexTooltip;
  dataLabels: ApexDataLabels;
};
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  dashboard = {
    categories: "Categorii",
    evaluations: "Evaluations"
  }

  public evaluationsChartOptions: Partial<TodaysEvaluations>;

  constructor(public translate: I18nService,
              private evaluationService: EvaluationService,
    ) {
      
      const currentMonth = new Date().getMonth();
      const currentYear = new Date().getFullYear().toString();
      const months = ["Ian ", "Feb ", "Mar ", "Apr ", "Mai ", "Iun ", "Iul ", "Aug ", "Sep ", "Oct ", "Noi ", "Dec "];

      this.evaluationsChartOptions = {
        series: [],
        chart: {
          height: 350,
          type: "area",
          toolbar: {
            export: {
              csv: {
                filename: "Grafic Evaluări 360",
                headerCategory: this.dashboard.categories
              },
              svg: {
                filename: "Grafic Evaluări 360",
              },
              png: {
                filename: "Grafic Evaluări 360",
              }
            }
          }
        },
        dataLabels: {
          enabled: false
        },
        stroke: {
          curve: "smooth"
        },
        xaxis: {
          type: "category",
          categories: months.slice(currentMonth + 1).map(month => month + (parseInt(currentYear) - 1))
              .concat(months.slice(0, currentMonth + 1).map(month => month + currentYear))
        }
      };
    }

  ngOnInit(): void {
    this.translateData();
    this.subscribeForLanguageChange();
    this.countEvaluations360().subscribe(series => {this.evaluationsChartOptions.series = series;});
  }

  translateData(): void {
		forkJoin([
      this.translate.get('dashboard.categories'),
			this.translate.get('dashboard.evaluations'),
		]).subscribe(
			([categories, evaluations]) => {
        this.dashboard.categories = categories;
				this.dashboard.evaluations = evaluations;
			}
		);
	}

	subscribeForLanguageChange(): void {
		this.translate.change.subscribe(() => this.translateData());
	}

  countEvaluations360() {
    return forkJoin([
      this.evaluationService.getNrEvaluations()
    ]).pipe(map(([evaluations]) => [
      {
        name: this.dashboard.evaluations,
        data: evaluations.data
      }
    ]));
  }
}