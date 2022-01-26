import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConvertPdfDocumentModalComponent } from '../../../utils/modals/convert-pdf-document-modal/convert-pdf-document-modal.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  constructor(private modalService: NgbModal,) { }

  ngOnInit(): void {
  }

  openConvertPdfModal(): void {
    const modalRef = this.modalService.open(ConvertPdfDocumentModalComponent, { centered: true, size: 'md'});
  }
}
