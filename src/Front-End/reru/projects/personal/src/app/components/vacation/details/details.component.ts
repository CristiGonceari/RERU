import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { differenceInCalendarDays } from 'date-fns';
import { VacationModel } from '../../../utils/models/vacation.model';
import { VacationService } from '../../../utils/services/vacation.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  isLoading: boolean = true;
  vacation: VacationModel;

  constructor(private vacationService: VacationService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.retrieveVacation(response.id);
      }
    });
  }

  retrieveVacation(id: number): void {
    this.vacationService.get(id).subscribe(response => {
      this.vacation = response.data;
      this.isLoading = false;
    });
  }

  getDays(): number {
    return differenceInCalendarDays(new Date(this.vacation.toDate), new Date(this.vacation.fromDate)) ;
  }

  downloadRequest(): void {
    this.vacationService.downloadRequest(this.vacation.id).subscribe(response => {
      const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
      const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    });
  }

  downloadOrder(): void {}
}
