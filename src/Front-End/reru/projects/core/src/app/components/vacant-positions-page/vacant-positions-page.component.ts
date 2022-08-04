import { Component, OnInit } from '@angular/core';
import { I18nService } from '../../utils/services/i18n.service';
import { CandidatePositionService } from '../../utils/services/candidate-position.service';
import { PaginationSummary } from '../../utils/models/pagination-summary.model';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';

@Component({
  selector: 'app-vacant-positions-page',
  templateUrl: './vacant-positions-page.component.html',
  styleUrls: ['./vacant-positions-page.component.scss']
})
export class VacantPositionsPageComponent implements OnInit {
  isLoading = true;
  positions = [];
  pagination: PaginationSummary = new PaginationSummary();
  content: string ='';
  public Editor = DecoupledEditor;
  year = new Date().getFullYear();
  currentLanguage: string;
  languageList = [
    { code: 'en', label: 'English' },
    { code: 'ro', label: 'Română' },
    { code: 'ru', label: 'Русский' },
  ];

  constructor(public translate: I18nService, private positionService: CandidatePositionService) { }

  ngOnInit(): void {
    this.get();
  }

  get(data: any = {}){
    const params: any = {
			name: data.name || '',
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		};

    this.positionService.getList(params).subscribe(res => {
      if (res && res.data) {
        this.positions = res.data.items;
        this.pagination = res.data.pagedSummary;
        this.isLoading = false;
      }
    })
  }

  getLang(): string {
    this.currentLanguage = this.translate.currentLanguage;
    const value = this.languageList.find(l => l.code == this.currentLanguage);
    return (value.label) || "Language";
  }

  useLanguage(language: string) {
    this.translate.use(language);
  }
}
