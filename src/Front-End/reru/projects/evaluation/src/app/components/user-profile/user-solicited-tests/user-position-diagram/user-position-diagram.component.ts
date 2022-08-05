import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestResultStatusEnum } from '../../../../utils/enums/test-result-status.enum';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { CandidatePositionService } from '../../../../utils/services/candidate-position/candidate-position.service';
import { PrintTemplateService } from '../../../../utils/services/print-template/print-template.service';
import { saveAs } from 'file-saver';
import { Location } from '@angular/common';

@Component({
  selector: 'app-user-position-diagram',
  templateUrl: './user-position-diagram.component.html',
  styleUrls: ['./user-position-diagram.component.scss']
})
export class UserPositionDiagramComponent implements OnInit {
  isLoading = true;
  eventsDiagram = [];
  userDiagram;
  positionId;
  positionName;
  userId;
  status = TestStatusEnum;
  result = TestResultStatusEnum;

  constructor(
    private positionService: CandidatePositionService,
    private activatedRoute: ActivatedRoute,
    private printService: PrintTemplateService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.positionId = params.id;
      this.positionService.get(this.positionId).subscribe(res => this.positionName = res.data.name);
    });
    this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.userId = params.id;
        this.getUserDiagram();
			}
		});
  }

  getUserDiagram(): void {
    this.positionService.getUserDiagram({ positionId: this.positionId, userId: this.userId }).subscribe(res => {
      if (res && res.data) {
        this.eventsDiagram = res.data.eventsDiagram;
        this.userDiagram = res.data.userDiagram;
        this.isLoading = false;
      }
    })
  }
  
  getTests(testTemplateId, eventId){
    let test = [];
    test = this.userDiagram.testsByTestTemplate.filter(e => e.testTemplateId === testTemplateId && e.eventId == eventId);

    return test;
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

  back(){
    this.location.back();
  }
}
