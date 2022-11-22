import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ViewEncapsulation } from '@angular/core';
import { EnumStringTranslatorService } from '../../services/enum-string-translator.service';
import { TestStatusEnum } from '../../enums/test-status.enum';
import { ActivatedRoute, Router } from '@angular/router';

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
  isActive: boolean = true;
  status = TestStatusEnum;

  constructor(private activeModal: NgbActiveModal,
    private enumStringTranslatorService: EnumStringTranslatorService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
  }

  translateResultValue(item) {
    return this.enumStringTranslatorService.translateTestResultValue(item);
  }

  openAddTest(value) {
    let data = {
      isOpenAddTest: true,
      selectedEventId: value.eventId,
      selectedTestTemplateId: value.testTemplateId
    }

    this.activeModal.close(data);
  }

  close(): void {
    this.activeModal.dismiss();
  }

  routeToUserProfileSolicitedTests(userId) {
    this.router.navigate(['../../../../user-profile/', userId, 'solicited-tests'], { relativeTo: this.route });
    this.close();
  }
}
