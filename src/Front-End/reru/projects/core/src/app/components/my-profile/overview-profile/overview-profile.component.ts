import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { MyProfile } from '../../../utils/models/user-profile.model';
import { I18nService } from '../../../utils/services/i18n.service';
import { ProfileService } from '../../../utils/services/profile.service';
import { saveAs } from 'file-saver';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PrintModalComponent } from '@erp/shared';

@Component({
	selector: 'app-overview-profile',
	templateUrl: './overview-profile.component.html',
	styleUrls: ['./overview-profile.component.scss'],
})
export class OverviewProfileComponent implements OnInit {
	isLoading = true;
	profileData = new MyProfile();
	headersToPrint = [];
	printTranslates: any[];
	downloadFile: boolean;
		
	constructor(private profileService: ProfileService, 
				public translate: I18nService,
				private modalService: NgbModal
		) {}

	ngOnInit(): void {
		this.initData();
	}

	getTitle(): string {
		return document.getElementById('title').innerHTML;
	}

	initData(): void {
		this.profileService.getUserProfile().subscribe(res => {
			if (res) {
				this.profileData = res.data;
				this.isLoading = false;
			}
		});
	}

	navigate(url: string): void {
		window.open(url, '_blank');
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['-', 'module', 'role'];
         
		for (let i = 1; i <= headersHtml.length - 2; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}

		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2
		};

		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'lg' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format', 'max-print-rows']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel'),
			this.translate.get('print.select-file-format'),
		  	this.translate.get('print.max-print-rows')
		]).subscribe(
			(items) => {
				for (let i = 0; i < this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		this.profileService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(2).slice(0, -2);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}
}
