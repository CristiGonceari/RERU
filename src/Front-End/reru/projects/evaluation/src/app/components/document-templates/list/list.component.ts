import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConvertPdfDocumentModalComponent } from '../../../utils/modals/convert-pdf-document-modal/convert-pdf-document-modal.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  title: string;
  @ViewChild('status') searchStatus: any;
  @ViewChild('documentName') documentName: any;

  constructor(private modalService: NgbModal,) { }

  ngOnInit(): void {
  }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

  clearFields() {
    this.documentName.value= "";
		this.searchStatus.getDocumentTemplatesType();
	}

  openConvertPdfModal(): void {
    const modalRef = this.modalService.open(ConvertPdfDocumentModalComponent, { centered: true, size: 'md'});
  }
}
