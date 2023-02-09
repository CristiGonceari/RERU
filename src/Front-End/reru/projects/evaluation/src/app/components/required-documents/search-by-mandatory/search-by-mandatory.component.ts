import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-search-by-mandatory',
  templateUrl: './search-by-mandatory.component.html',
  styleUrls: ['./search-by-mandatory.component.scss']
})
export class SearchByMandatoryComponent{

  @Output() filter: EventEmitter<void> = new EventEmitter<void>();

}
