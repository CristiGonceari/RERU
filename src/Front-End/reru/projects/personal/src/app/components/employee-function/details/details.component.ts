import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeFunctionModel } from '../../../utils/models/employee-function.model';
import { EmployeeFunctionService } from '../../../utils/services/employee-function.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {

  isLoading: boolean = true;
  function: EmployeeFunctionModel;

  constructor( 
    private route: ActivatedRoute,
    private router: Router,
    private ngZone: NgZone,
    private employeeFunctionService: EmployeeFunctionService
    ) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id && !isNaN(response.id)) {
        this.retrieveFunction(response.id);
      } else {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
      }
    });
  }

  retrieveFunction(id: number): void {
    this.employeeFunctionService.get(id).subscribe(response => {
      if (!response) {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
        return;
      }
      this.function = response.data;
      this.isLoading = false;
    });
  }
}
