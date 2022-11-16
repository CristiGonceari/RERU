import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ViewEncapsulation } from '@angular/core';
import { EnumStringTranslatorService } from '../../services/enum-string-translator.service';
import { TestStatusEnum } from '../../enums/test-status.enum';

@Component({
  selector: 'app-view-position-diagram-modal',
  templateUrl: './view-position-diagram-modal.component.html',
  styleUrls: ['./view-position-diagram-modal.component.scss'],
  encapsulation: ViewEncapsulation.None,
})

export class ViewPositionDiagramModalComponent implements OnInit {

  eventsDiagram = [];
  usersDiagram = [];
  testTemplates = [];

  status = TestStatusEnum;

  constructor(private activeModal: NgbActiveModal,
    private enumStringTranslatorService: EnumStringTranslatorService,
    ) { }

  ngOnInit(): void {
  }

  translateResultValue(item) {
    return this.enumStringTranslatorService.translateTestResultValue(item);
  }

  close(): void {
    this.activeModal.close();
  }
}
