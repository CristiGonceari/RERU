import { Component } from '@angular/core';
import { I18nService } from '../../utils/services/i18n.service';
import {
  ApexNonAxisChartSeries,
  ApexPlotOptions,
  ApexChart,
  ApexFill,
  ApexAxisChartSeries,
  ApexXAxis,
  ApexStroke,
  ApexTooltip,
  ApexDataLabels
} from "ng-apexcharts";

export type NotificationsChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  labels: string[];
  plotOptions: ApexPlotOptions;
  fill: ApexFill;
};

export type TodaysEvaluations = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
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
  notificationsChart: NotificationsChartOptions;
  evaluationsChartOptions: TodaysEvaluations;
  constructor(private readonly translate: I18nService) {
    this.notificationsChart = {
      series: [25],
      chart: {
        height: 250,
        type: "radialBar"
      },
      plotOptions: {
        radialBar: {
          hollow: {
            size: "70%"
          },
          dataLabels: {
            show: true,
            name: {
                color: '#1BC5BD'
            },
            value: {
              formatter: () => '25/100'
            }
          }
        }
      },
      labels: ["New notifications"],
      fill: {
        colors: ['#1BC5BD']
      }
    };

    this.evaluationsChartOptions = {
      series: [
        {
          name: "MAI Patrulare",
          data: [31, 40, 28, 51, 42, 109, 100, 90, 80, 70, 60, 100, 50, 40]
        },
        {
          name: "MAI Criminalistica",
          data: [11, 32, 45, 32, 34, 52, 41, 55, 61, 55, 45, 41, 39, 14]
        }
      ],
      chart: {
        height: 350,
        type: "area"
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "smooth"
      },
      xaxis: {
        type: "datetime",
        categories: [
          "2022-11-24T08:00:00.000Z",
          "2022-11-24T09:00:00.000Z",
          "2022-11-24T10:00:00.000Z",
          "2022-11-24T11:00:00.000Z",
          "2022-11-24T12:00:00.000Z",
          "2022-11-24T13:00:00.000Z",
          "2022-11-24T14:00:00.000Z",
          "2022-11-24T15:00:00.000Z",
          "2022-11-24T16:00:00.000Z",
          "2022-11-24T17:00:00.000Z",
          "2022-11-24T18:00:00.000Z",
          "2022-11-24T19:00:00.000Z",
          "2022-11-24T20:00:00.000Z",
          "2022-11-24T21:00:00.000Z",
        ]
      },
      tooltip: {
        x: {
          format: "dd/MM/yy HH:mm"
        }
      }
    };
  }
 }