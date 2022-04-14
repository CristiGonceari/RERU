import { Component, OnInit} from '@angular/core';
import { DocumentTemplateService } from '../../../utils/services/document-template/document-template.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import { PaginationModel } from '../../models/pagination.model';
import { SelectItem } from '../../models/select-item.model';
import { ReferenceService } from '../../services/reference/reference.service';


@Component({
  selector: 'app-convert-pdf-document-modal',
  templateUrl: './convert-pdf-document-modal.component.html',
  styleUrls: ['./convert-pdf-document-modal.component.scss']
})

export class ConvertPdfDocumentModalComponent implements OnInit {

  selectedType: number = 0;
  isLoading: boolean = true;
  documents: any[] = [];
  fileType: SelectItem;

  pagedSummary: PaginationModel = new PaginationModel();

  constructor(private documentService: DocumentTemplateService,
              private activeModal: NgbActiveModal,
              private referenceService: ReferenceService,
    ) { }

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
    this.getTemplateType();
  }

  retrieveDocuments(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      fileType: this.selectedType,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
    });
    this.documentService.list(request).subscribe(response => {
      this.documents = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
    })
  }

  getTemplateType(){
    this.referenceService.getDocumentTemplateType().subscribe(res => {
        this.fileType = res.data;
    })
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}


