import { Component, OnInit } from '@angular/core';
import { UserProfileService } from './../../utils/services/user-profile/user-profile.service';
import { saveAs } from 'file-saver';
import { CloudFileService } from '../../utils/services/cloud-file/cloud-file.service';
import { NotificationUtil } from '../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { PaginationModel } from '../../utils/models/pagination.model';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import {
  ApexChart,
  ApexFill,
  ApexAxisChartSeries,
  ApexXAxis,
  ApexStroke,
  ApexTooltip,
  ApexDataLabels,
  ApexYAxis
} from "ng-apexcharts";
import { forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';
import { I18nService } from '../../utils/services/i18n/i18n.service';

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
  dashboard = {
    tests: "Tests",
    evaluations: "Evaluations"
  }

  // Apex Chart
  public evaluationsChartOptions: Partial<TodaysEvaluations>;

  constructor(private userService: UserProfileService,
              private fileService : CloudFileService,
              private notificationService: NotificationsService,
              private testService: TestService,
              public translate: I18nService
    ) {
      
      const currentMonth = new Date().getMonth();
      const currentYear = new Date().getFullYear().toString();
      const months = ["Ian ", "Feb ", "Mar ", "Apr ", "Mai ", "Iun ", "Iul ", "Aug ", "Sep ", "Oct ", "Noi ", "Dec "];

      this.evaluationsChartOptions = {
        series: [],
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
          type: "category",
          categories: months.slice(currentMonth + 1).map(month => month + (parseInt(currentYear) - 1))
              .concat(months.slice(0, currentMonth + 1).map(month => month + currentYear))
        }
      };
    }

  ngOnInit(): void {
    this.translateData();
    this.subscribeForLanguageChange();

    this.retrieveProfile();
    this.getDemoList();
    this.countTestsAndEvaluations().subscribe(series => {this.evaluationsChartOptions.series = series;});
  }

  translateData(): void {
		forkJoin([
			this.translate.get('dashboard.tests'),
			this.translate.get('dashboard.evaluations'),
		]).subscribe(
			([ tests, evaluations ]) => {
				this.dashboard.tests = tests;
				this.dashboard.evaluations = evaluations;
			}
		);
	}

	subscribeForLanguageChange(): void {
		this.translate.change.subscribe(() => this.translateData());
	}

  countTestsAndEvaluations() {
    return forkJoin([
      this.testService.getNrTests(),
      this.testService.getNrEvaluations()
    ]).pipe(map(([tests, evaluations]) => [
      {
        name: this.dashboard.tests,
        data: tests.data
      },
      {
        name: this.dashboard.evaluations,
        data: evaluations.data
      }
    ]));
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