import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PrintModalComponent } from '@erp/shared';
import { TestResultStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-result-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { EnumStringTranslatorService } from 'projects/evaluation/src/app/utils/services/enum-string-translator.service';
import { saveAs } from 'file-saver';
import { forkJoin } from 'rxjs';
import { ObjectUtil } from '../../../utils/util/object.util';

@Component({
	selector: 'app-my-evaluations',
	templateUrl: './my-evaluations.component.html',
	styleUrls: ['../table-inherited.component.scss', 'my-evaluations.component.scss']
})
export class MyEvaluationsComponent implements OnInit {
	@ViewChild('evaluatedName') evaluatedName: any;
	fromDate: string;
	tillDate: string;
	testRowList: [] = [];
	pagedSummary: PaginationModel = new PaginationModel();
	userId: number;
	isLoading: boolean = true;
	enum = TestStatusEnum;
	resultEnum = TestResultStatusEnum;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
	title: string;
	filters: any = {};
	searchFrom: string;
	searchTo: string;

	constructor(
		private testService: TestService,
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
		private modalService: NgbModal,
		private enumStringTranslatorService: EnumStringTranslatorService
	) { }

	ngOnInit(): void {
		this.getUserTests();
	}

	translateResultValue(item){
		return this.enumStringTranslatorService.translateTestResultValue(item);
	}

	getUserTests(data: any = {}) {
		this.setTimeToSearch();
		const params: any = ObjectUtil.preParseObject({
			evaluatedName: this.filters.evaluatedName || '',
			fromDate: this.searchFrom || '',
			toDate: this.searchTo || '',
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
		})

		this.testService.getMyEvaluations(params).subscribe(
			res => {
				const response = {
					data: {
					  items: [
						{
						  id: 1398,
						  userId: 1150,
						  evaluatorId: 1123,
						  testTemplateId: 115,
						  eventName: null,
						  eventId: 0,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 2,
						  accumulatedPercentage: 0,
						  userName: "Cristina Candidat Anatolie",
						  idnp: "1002012457878",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Comisia medicală",
						  rules: null,
						  verificationProgress: "0/0",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 4,
						  modeStatus: 2,
						  result: 7,
						  resultValue: "Recommended:/1,2,3,4",
						  programmedTime: "2022-11-07T10:59:25.601104",
						  endProgrammedTime: null,
						  endTime: "2022-11-07T10:59:46.732162",
						  viewTestResult: null,
						  candidatePositionNames: null
						},
						{
						  id: 1393,
						  userId: 1150,
						  evaluatorId: 1123,
						  testTemplateId: 118,
						  eventName: null,
						  eventId: 0,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 1,
						  accumulatedPercentage: 0,
						  userName: "Cristina Candidat Anatolie",
						  idnp: "1002012457878",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Evaluare psihiatrică",
						  rules: null,
						  verificationProgress: "0/0",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 4,
						  modeStatus: 2,
						  result: 3,
						  resultValue: "Able",
						  programmedTime: "2022-11-07T10:11:52.035525",
						  endProgrammedTime: null,
						  endTime: "2022-11-07T10:13:24.709786",
						  viewTestResult: null,
						  candidatePositionNames: null
						},
						{
						  id: 1390,
						  userId: 1283,
						  evaluatorId: 1123,
						  testTemplateId: 113,
						  eventName: null,
						  eventId: 0,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 1,
						  accumulatedPercentage: 0,
						  userName: "Olun Victoria Emilian",
						  idnp: "1019607003010",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Evaluare psihologica",
						  rules: null,
						  verificationProgress: "-",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 2,
						  modeStatus: 2,
						  result: 0,
						  resultValue: "NoResult",
						  programmedTime: "2022-11-07T10:10:49.879623",
						  endProgrammedTime: null,
						  endTime: null,
						  viewTestResult: null,
						  candidatePositionNames: null
						},
						{
						  id: 1384,
						  userId: 1146,
						  evaluatorId: 1123,
						  testTemplateId: 118,
						  eventName: "Comisia medicală(3)",
						  eventId: 86,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 1,
						  accumulatedPercentage: 0,
						  userName: "Gordelenco Cris Pavel",
						  idnp: "1021600021760",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Evaluare psihiatrică",
						  rules: null,
						  verificationProgress: "-",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 2,
						  modeStatus: 2,
						  result: 0,
						  resultValue: "NoResult",
						  programmedTime: "2022-11-04T09:44:52.843167",
						  endProgrammedTime: "2022-11-30T11:21:19",
						  endTime: null,
						  viewTestResult: null,
						  candidatePositionNames: null
						},
						{
						  id: 1383,
						  userId: 1128,
						  evaluatorId: 1123,
						  testTemplateId: 118,
						  eventName: "Comisia medicală(3)",
						  eventId: 86,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 1,
						  accumulatedPercentage: 0,
						  userName: "Gonceari Cristian Veaceslav",
						  idnp: "2031012829776",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Evaluare psihiatrică",
						  rules: null,
						  verificationProgress: "-",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 0,
						  modeStatus: 2,
						  result: 0,
						  resultValue: "NoResult",
						  programmedTime: "2022-11-03T05:21:15.591",
						  endProgrammedTime: "2022-11-30T11:21:19",
						  endTime: null,
						  viewTestResult: null,
						  candidatePositionNames: null
						},
						{
						  id: 1382,
						  userId: 1150,
						  evaluatorId: 1123,
						  testTemplateId: 118,
						  eventName: "Comisia medicală(3)",
						  eventId: 86,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 1,
						  accumulatedPercentage: 0,
						  userName: "Cristina Candidat Anatolie",
						  idnp: "1002012457878",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Evaluare psihiatrică",
						  rules: null,
						  verificationProgress: "0/0",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 4,
						  modeStatus: 2,
						  result: 3,
						  resultValue: "Able",
						  programmedTime: "2022-11-04T09:43:42.675559",
						  endProgrammedTime: "2022-11-30T11:21:19",
						  endTime: "2022-11-04T09:43:52.168027",
						  viewTestResult: null,
						  candidatePositionNames: null
						},
						{
						  id: 1381,
						  userId: 1146,
						  evaluatorId: 1123,
						  testTemplateId: 113,
						  eventName: "DSE(3)",
						  eventId: 85,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 1,
						  accumulatedPercentage: 0,
						  userName: "Gordelenco Cris Pavel",
						  idnp: "1021600021760",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Evaluare psihologica",
						  rules: null,
						  verificationProgress: "-",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 0,
						  modeStatus: 2,
						  result: 0,
						  resultValue: "NoResult",
						  programmedTime: "2022-11-03T05:18:48.017",
						  endProgrammedTime: "2022-11-30T11:18:53",
						  endTime: null,
						  viewTestResult: null,
						  candidatePositionNames: null
						},
						{
						  id: 1380,
						  userId: 1128,
						  evaluatorId: 1123,
						  testTemplateId: 113,
						  eventName: "DSE(3)",
						  eventId: 85,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 1,
						  accumulatedPercentage: 0,
						  userName: "Gonceari Cristian Veaceslav",
						  idnp: "2031012829776",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Evaluare psihologica",
						  rules: null,
						  verificationProgress: "-",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 0,
						  modeStatus: 2,
						  result: 0,
						  resultValue: "NoResult",
						  programmedTime: "2022-11-03T05:18:48.017",
						  endProgrammedTime: "2022-11-30T11:18:53",
						  endTime: null,
						  viewTestResult: null,
						  candidatePositionNames: null
						},
						{
						  id: 1379,
						  userId: 1150,
						  evaluatorId: 1123,
						  testTemplateId: 113,
						  eventName: "DSE(3)",
						  eventId: 85,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 1,
						  accumulatedPercentage: 0,
						  userName: "Cristina Candidat Anatolie",
						  idnp: "1002012457878",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Evaluare psihologica",
						  rules: null,
						  verificationProgress: "-",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 0,
						  modeStatus: 2,
						  result: 0,
						  resultValue: "NoResult",
						  programmedTime: "2022-11-03T05:18:48.017",
						  endProgrammedTime: "2022-11-30T11:18:53",
						  endTime: null,
						  viewTestResult: null,
						  candidatePositionNames: null
						},
						{
						  id: 1378,
						  userId: 1150,
						  evaluatorId: 1123,
						  testTemplateId: 118,
						  eventName: "Comisia medicală(3)",
						  eventId: 86,
						  locationNames: [],
						  testPassStatus: null,
						  maxErrors: 1,
						  duration: 0,
						  minPercent: 0,
						  questionCount: 1,
						  accumulatedPercentage: 0,
						  userName: "Cristina Candidat Anatolie",
						  idnp: "1002012457878",
						  evaluatorName: "Mihaela-admin admin Burlacu Emilian",
						  evaluatorIdnp: "1012611000042",
						  testTemplateName: "Evaluare psihiatrică",
						  rules: null,
						  verificationProgress: "-",
						  showUserName: true,
						  isEvaluator: false,
						  testStatus: 2,
						  modeStatus: 2,
						  result: 0,
						  resultValue: "NoResult",
						  programmedTime: "2022-11-03T16:20:09.108859",
						  endProgrammedTime: "2022-11-30T11:21:19",
						  endTime: null,
						  viewTestResult: null,
						  candidatePositionNames: null
						}
					  ],
					  pagedSummary: {
						totalCount: 101,
						pageSize: 10,
						currentPage: 1,
						totalPages: 11
					  }
					},
					success: true,
					messages: []
				  } as any
				  this.testRowList = response.data.items;
				  this.pagedSummary = response.data.pagedSummary;
				  this.isLoading = false;
				// if (res && res.data) {
				// 	this.testRowList = res.data.items;
				// 	this.pagedSummary = res.data.pagedSummary;
				// 	this.isLoading = false;
				// }
			}, () => {
				this.isLoading = false;
			}
		)
	}

	setTimeToSearch(): void {
		if (this.fromDate) {
			const date1 = new Date(this.fromDate);
			this.searchFrom = new Date(date1.getTime() - (new Date(this.fromDate).getTimezoneOffset() * 60000)).toISOString();
		}
		if (this.tillDate) {
			const date2 = new Date(this.tillDate);
			this.searchTo = new Date(date2.getTime() - (new Date(this.tillDate).getTimezoneOffset() * 60000)).toISOString();
		}
	}

	setFilter(field: string, value): void {
		this.filters[field] = value;
		this.pagedSummary.currentPage = 1;
		this.getUserTests();
	}

	resetFilters(): void {
		this.filters = {};
		this.evaluatedName.key = '';
		this.searchFrom = '';
		this.searchTo = '';
		this.fromDate = '';
		this.tillDate = '';
		this.pagedSummary.currentPage = 1;
		this.getUserTests();
	}

	getHeaders(name: string): void {
		this.translateData();
		let evaluatedTestTable = document.getElementById('evaluatedTestTable')
		let headersHtml = evaluatedTestTable.getElementsByTagName('th');
		let headersDto = ['programmedTime', 'testStatus', 'testTemplateName', 'accumulatedPercentage', 'result'];
		for (let i = 0; i < headersHtml.length; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			userId: this.userId
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}


	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel')
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
		this.testService.printUserEvaluatedTests(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}
}
