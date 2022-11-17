import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestResultStatusEnum } from '../../../../utils/enums/test-result-status.enum';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { CandidatePositionService } from '../../../../utils/services/candidate-position/candidate-position.service';
import { PrintTemplateService } from '../../../../utils/services/print-template/print-template.service';
import { saveAs } from 'file-saver';
import { MedicalColumnEnum } from '../../../../utils/enums/medical-column.enum';
import { EnumStringTranslatorService } from '../../../../utils/services/enum-string-translator.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ViewPositionDiagramModalComponent } from 'projects/evaluation/src/app/utils/modals/view-position-diagram-modal/view-position-diagram-modal.component';

@Component({
  selector: 'app-diagram',
  templateUrl: './diagram.component.html',
  styleUrls: ['./diagram.component.scss']
})

export class DiagramComponent implements OnInit {
  isLoading = true;
  eventsDiagram = [];
  usersDiagram = [];
  testTemplates = [];
  positionId;
  positionName;
  positionMedicalColumn;
  events = [];
  medicalColumnEnum = MedicalColumnEnum;
  status = TestStatusEnum;
  result = TestResultStatusEnum;

  isOpenAddTest: boolean = false;

  selectedEventId;
  selectedTestTemplateId;

  constructor(
    private positionService: CandidatePositionService,
    private activatedRoute: ActivatedRoute,
    private printService: PrintTemplateService,
    private enumStringTranslatorService: EnumStringTranslatorService,
    public modalService: NgbModal,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.parent.params.subscribe(params => {
      this.positionId = params.id;
      this.positionService.get(this.positionId).subscribe(res => {
        this.positionName = res.data.name;
        this.positionMedicalColumn = res.data.medicalColumn;
        this.events = res.data.events;

        if (this.events.length) this.getDiagram();
        else this.isLoading = false;
      });
    });
  }

  getDiagram(): void {
    this.isLoading = true;

    this.positionService.getDiagram({ positionId: this.positionId }).subscribe(res => {
      if (res && res.data) {
        this.eventsDiagram = res.data.eventsDiagram;
        this.usersDiagram = res.data.usersDiagram;

        this.eventsDiagram.forEach(event => {
          event.testTemplates.forEach(element => {
            this.testTemplates.push(element)
          });
        });
        this.isLoading = false;
      }
    })
  }

  translateResultValue(item) {
    return this.enumStringTranslatorService.translateTestResultValue(item);
  }

  printPositionDiagram() {
    this.printService.getPositionDiagramPdf(this.positionId).subscribe((response: any) => {
      let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];

      if (response.body.type === 'application/pdf') {
        fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
      }

      const blob = new Blob([response.body], { type: response.body.type });
      const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    });
  }

  openFullScreenMode() {
    const modalRef: any = this.modalService.open(ViewPositionDiagramModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.eventsDiagram = this.eventsDiagram;
    modalRef.componentInstance.usersDiagram = this.usersDiagram;
    modalRef.componentInstance.testTemplates = this.testTemplates;
    modalRef.result.then(() => {
      if ((modalRef.result?.__zone_symbol__value?.isOpenAddTest &&
        modalRef.result?.__zone_symbol__value?.selectedEventId &&
        modalRef.result?.__zone_symbol__value?.selectedTestTemplateId) != null
      ) {
        this.isOpenAddTest = modalRef.result.__zone_symbol__value.isOpenAddTest;
        this.selectedEventId = modalRef.result.__zone_symbol__value.selectedEventId;
        this.selectedTestTemplateId = modalRef.result.__zone_symbol__value.selectedTestTemplateId;
      }
    }, () => { })
  }

  openAddTest(value) {
    this.isOpenAddTest = true;
    this.selectedEventId = value.eventId;
    this.selectedTestTemplateId = value.testTemplateId;
  }

  onChangeAddTest(value: boolean) {
    this.isOpenAddTest = value;
    this.clearDiagramData();
    this.getDiagram();
  }

  clearDiagramData() {
    this.eventsDiagram = [];
    this.usersDiagram = [];
    this.testTemplates = [];
  }
}
