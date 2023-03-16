import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../services/i18n/i18n.service';
import { LoggingService } from '../../services/logging-service/logging.service';

@Component({
  selector: 'app-search-module',
  templateUrl: './search-module.component.html',
  styleUrls: ['./search-module.component.scss']
})
export class SearchModuleComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  projects;
  
  constructor(
    private loggingService: LoggingService, 
    public translate: I18nService) 
    { 
      this.retriveDropdowns(); 
    }

  retriveDropdowns() {
    this.loggingService.getProjectSelectItem().subscribe((res) => (this.projects = res.data));
  }
}
