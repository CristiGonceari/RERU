import { Component, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddHolidayModalComponent } from '../../utils/modals/add-holiday-modal/add-holiday-modal.component';
import { ConfigureHolidayComponent } from './configure-holiday/configure-holiday.component';

@Component({
  selector: 'app-configurations',
  templateUrl: './configurations.component.html',
  styleUrls: ['./configurations.component.scss']
})
export class ConfigurationsComponent {
  @ViewChild(ConfigureHolidayComponent) configureHolidayComponent;
  constructor(private modalService: NgbModal) { }

  openAddHolidayModal(): void {
    this.modalService.open(AddHolidayModalComponent).result.then(response => {
      this.configureHolidayComponent.addHoliday(response);
    }, () => {});
  }
}
