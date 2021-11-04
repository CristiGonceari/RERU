import { Component, OnInit } from '@angular/core';
import { I18nService } from '../../utils/services/i18n/i18n.service';

@Component({
	selector: 'app-layouts',
	templateUrl: './layouts.component.html',
	styleUrls: ['./layouts.component.scss']
})
export class LayoutsComponent implements OnInit {
	constructor(private translate: I18nService) {}

	ngOnInit(): void {
	  alert('Personal worksksks!')
	}

}
