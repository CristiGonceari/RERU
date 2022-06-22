import { Injectable } from "@angular/core";
import { NgbDateParserFormatter, NgbDateStruct } from "@ng-bootstrap/ng-bootstrap";

@Injectable({
    providedIn: 'root'
})
export class NgbDateFRParserFormatter extends NgbDateParserFormatter {
    parse(value: string | Date): NgbDateStruct {
        if (value && Date.parse(<string>value) && typeof (<Date>value).getDate === 'function') {
            const day = +(<Date>value).getDate();
            const month = +(<Date>value).getMonth() + 1;
            const year = +(<Date>value).getFullYear();
            return { year , month, day };
        }
    }

    format(date: NgbDateStruct): string {
        if (!date) {
            return '';
        }

        const day = date.day < 10 ? '0' + date.day : date.day;
        const month = date.month < 10 ? '0' + date.month : date.month;

        return `${day}.${month}.${date.year}`;
    }
}