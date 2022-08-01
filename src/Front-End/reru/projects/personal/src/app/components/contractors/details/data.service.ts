import { Observable, Subject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class DataService {
    private subject = new Subject<mapModel>();
 
    sendData(data) {
        this.subject.next(data);
    }
 
    clearData() {
        this.subject.next();
    }
 
    getData(): Observable<mapModel> {
        return this.subject.asObservable();
    }
}

export interface mapModel{
    isDone: boolean;
    stepId: number;
}