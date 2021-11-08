import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class QuestionByCategoryService {
	private selectedData: BehaviorSubject<any> = new BehaviorSubject(null);
	selected = this.selectedData.asObservable();

	private set: BehaviorSubject<boolean> = new BehaviorSubject(null);
	value = this.set.asObservable();

	constructor() {	}

	setData = (value: any) => {this.selectedData.next(value)};
	setValue = (value: any) => {this.set.next(value)};
}
