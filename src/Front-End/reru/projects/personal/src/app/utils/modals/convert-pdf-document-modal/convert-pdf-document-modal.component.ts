import { Component, OnInit} from '@angular/core';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { DocumentsTemplateService } from '../../../utils/services/documents-template.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import { DocumentGeneratorService } from '../../services/document-generator.service'


@Component({
  selector: 'app-convert-pdf-document-modal',
  templateUrl: './convert-pdf-document-modal.component.html',
  styleUrls: ['./convert-pdf-document-modal.component.scss']
})

export class ConvertPdfDocumentModalComponent implements OnInit {

  selectedType: number = 0;
  isLoading: boolean = true;
  documents: any[] = [];

  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };

  constructor(private documentService: DocumentsTemplateService,
              private documentGeneratorService: DocumentGeneratorService,
              private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.getList();
  }

  getList(data :any = {}): void {
    this.isLoading = true;
    const request= ObjectUtil.preParseObject({
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage:data.itemsPerPage || this.pagedSummary.pageSize
    })
    this.documentService.list(request).subscribe(response => {
      if (response.success) {
        this.documents = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  retrieveDocuments(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      file: this.selectedType,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
    });
    this.documentGeneratorService.getDocuments(request).subscribe(response => {
      this.documents = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
    })
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}


