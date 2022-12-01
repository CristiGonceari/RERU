import { Component, OnInit } from '@angular/core';
import { UserProfileService } from './../../utils/services/user-profile/user-profile.service';
import { saveAs } from 'file-saver';
import { CloudFileService } from '../../utils/services/cloud-file/cloud-file.service';
import { NotificationUtil } from '../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { PaginationModel } from '../../utils/models/pagination.model';
import { HttpEvent, HttpEventType } from '@angular/common/http';
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
export class DashboardComponent implements OnInit {
  fill: ApexFill = {
    colors: ['#F64E60']
  }
  profile;
  isLoading: boolean = true;
  fileId;
  seIncarca: boolean = true;
  seIncarca1: boolean = true;
  seIncarca3: boolean = true;
  isLoadingTable: boolean = true;
  fileType;
  files:[] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  lastId;
  uploadFiles;
  fileIdForDelete;

  isLoadingSent: boolean = false;

  fileStatus = {status: '', requestType: '', percent: 0 }
  filenames: string[] = [];

  // Apex Chart
  public notificationsChart: Partial<NotificationsChartOptions>;
  public evaluationsChartOptions: Partial<TodaysEvaluations>;

  constructor(private userService: UserProfileService,
              private fileService : CloudFileService,
              private notificationService: NotificationsService,
    ) {
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

  ngOnInit(): void {
    this.retrieveProfile();
    this.getDemoList();
    // this.evaluationsChartOptions.series = this.generateData(10, 100, 15);
  }

  generateData(baseval, count, yrange) {
    var i = 0;
    var series = [];
    while (i < count) {
      var x = Math.floor(Math.random() * (750 - 1 + 1)) + 1;
      var y =
        Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min;
      var z = Math.floor(Math.random() * (75 - 15 + 1)) + 15;

      series.push([x, y, z]);
      baseval += 86400000;
      i++;
    }
    return series;
  }

  retrieveProfile(): void {
    this.userService.getCurrentUser().subscribe(response => {
      this.profile = response;
      this.isLoading = false;
    })
  }

  downloadFile(item): void {
    this.seIncarca = false;
    this.fileService.get(item).subscribe(
      event => {
      // const fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0].split('"')[1];
      // const blob = new Blob([response.body], { type: response.body.type });
      // const file = new File([blob], fileName, { type: response.body.type });
      // saveAs(file);
      // this.seIncarca = true;
      this.resportProggress(event);
    });
  }
  
  private resportProggress(httpEvent: HttpEvent<string[] | Blob>): void
  {
    switch(httpEvent.type )
    { 
      case HttpEventType.Sent:
          this.isLoadingSent = true;
        break;
      case HttpEventType.UploadProgress:
        this.updateStatus(httpEvent.loaded, httpEvent.total, 'Uploading...')
        break;
      case HttpEventType.DownloadProgress:
          this.isLoadingSent = false;
          this.updateStatus(httpEvent.loaded, httpEvent.total, 'Dowloading...')
        break;
      case HttpEventType.ResponseHeader:
          console.log("Returned Header", httpEvent)
        break;
      case HttpEventType.Response:
          if (httpEvent.body instanceof Array) {
            this.fileStatus.status = 'done';
            for (const filename of httpEvent.body) {
              this.filenames.unshift(filename);
            }
          } else {
            const fileName = httpEvent.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
            const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
            const file = new File([blob], fileName, { type: httpEvent.body.type });
            saveAs(file);
          }
          this.fileStatus.status = 'done';
          this.fileStatus.percent = 0;
          break;
    }
  }
  updateStatus(loaded: number, total: number | undefined, requestType: string)
  {
    this.fileStatus.status = "progress";
    this.fileStatus.requestType = requestType;
    this.fileStatus.percent = Math.round(100 * loaded / total);
  }

  deleteFile(id):void
  {
    this.fileService.delete(id).subscribe(res => {
      this.notificationService.success('Success', 'Was deleted', NotificationUtil.getDefaultConfig());
      this.getDemoList();
    })
    
  }
  
  uploadFile(): void
  {
    this.seIncarca1 = false;
    const request = new FormData();
    request.append('File', this.uploadFiles);
    request.append('Type', this.fileType);
    this.fileService.create(request).subscribe(res => {
      this.lastId = res.data;
      this.notificationService.success('Success', 'Fișier adăugat!', NotificationUtil.getDefaultConfig());
      this.seIncarca1 = true;
      this.getDemoList();
    }, error =>
    {
      this.notificationService.warn('Error', NotificationUtil.getDefaultConfig());
      this.seIncarca1 = true;
    })
  }
  
  onFileChange(event){
    this.uploadFiles = event.target.files[0];
  }

  getDemoList(data: any = {}) {

		this.fileService.list().subscribe(
			res => {
				if (res && res.data) {
					this.files = res.data;
					this.pagedSummary = res.data.pagedSummary;
					this.isLoadingTable = false;
				}
			}
		)
	}
  
}
