import { Component, EventEmitter, Output } from '@angular/core';

@Component({
	selector: 'app-permission-search',
	templateUrl: './permission-search.component.html',
	styleUrls: ['./permission-search.component.scss'],
})
export class PermissionSearchComponent {
	key: string;
	@Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();

	constructor() {}

	search(value: string): void {
		this.handleSearch.emit(value);
	}
}
