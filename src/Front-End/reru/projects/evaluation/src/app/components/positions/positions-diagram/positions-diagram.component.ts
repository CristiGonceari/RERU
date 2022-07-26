import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestResultStatusEnum } from '../../../utils/enums/test-result-status.enum';
import { TestStatusEnum } from '../../../utils/enums/test-status.enum';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { CandidatePositionService } from '../../../utils/services/candidate-position/candidate-position.service';

@Component({
  selector: 'app-positions-diagram',
  templateUrl: './positions-diagram.component.html',
  styleUrls: ['./positions-diagram.component.scss']
})
export class PositionsDiagramComponent implements OnInit {
  isLoading = true;
  eventsDiagram = [];
  usersDiagram = [];
  testTemplates = [];
  pagination: PaginationModel = new PaginationModel();
  positionId;
  status = TestStatusEnum;
  result = TestResultStatusEnum;

  constructor(
    private positionService: CandidatePositionService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.positionId = params.id;
    });
    this.getDiagram();
  }

  getDiagram(): void {
    this.positionService.getDiagram({ positionId: this.positionId }).subscribe(res => {
      if (res && res.data) {
        this.eventsDiagram = res.data.eventsDiagram;
        this.usersDiagram = res.data.usersDiagram;

        this.eventsDiagram.forEach(event => {
          if (event.testTemplates.length == 0) this.testTemplates.push({ template: "", eventId: event.eventId })

          event.testTemplates.forEach(element => {
            this.testTemplates.push({ template: element, eventId: event.eventId })
          });
        });

        this.testTemplates.forEach(testTemplate => {
          this.usersDiagram.forEach(userProfile => {
            userProfile.testsByTestTemplate.find(x => x.testTemplateId == testTemplate.template.testTemplateId && x.eventId == testTemplate.template.eventId);

            if (userProfile.testsByTestTemplate.length < this.testTemplates.length) {
              if (userProfile.testsByTestTemplate.some(x => x.testTemplateId != testTemplate.testTemplateId))
                userProfile.testsByTestTemplate.push({ testTemplateId: testTemplate.template.testTemplateId, eventId: testTemplate.eventId, tests: [] })
            }

            userProfile.testsByTestTemplate.sort((a, b) => (a.testTemplateId < b.testTemplateId) ? -1 : 1)
            userProfile.testsByTestTemplate.sort((a, b) => (a.eventId < b.eventId) ? -1 : 1)
          });
        })
        this.isLoading = false;
      }
    })
  }
}
