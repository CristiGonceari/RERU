import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddTestHistoryModalComponent } from '../../../utils/modals/add-test-history-modal/add-test-history-modal.component';
import { ReferenceService } from '../../../utils/services/reference/reference.service';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.scss']
})
export class TestListComponent implements OnInit {

  title: string;
  processesData: any;
  interval: any;

  @ViewChild('testName') testName: any;
  @ViewChild('testEvent') testEvent: any;
  @ViewChild('testLocation') testLocation: any;
  @ViewChild('userName') userName: any;
  @ViewChild('userEmail') userEmail: any;
  @ViewChild('idnp') idnp: any;
  @ViewChild('selectedStatus') selectedStatus: any;
  @ViewChild('selectedResult') selectedResult: any;

  constructor(
    private referenceService: ReferenceService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.referenceService.getProcesses().subscribe(res => {
      this.processesData = res.data;

      if (this.processesData && !this.processesData.isDone) {
        this.interval = setInterval(() => {
          this.referenceService.getProcesses().subscribe(res => {
            this.processesData = res.data;

            if (this.processesData.length <= 0) {
              clearInterval(this.interval);
            }
          })
        }, 10 * 300);
      }
    })
  }

  getTitle(): string {
    this.title = document.getElementById('title').innerHTML;
    return this.title
  }

  clearFields() {
    this.testName.key = '';
    this.testEvent.key = '';
    this.testLocation.key = '';
    this.userName.key = '';
    this.userEmail.key = '';
    this.idnp.key = '';
    this.selectedStatus.getTestStatuses();
    this.selectedResult.getTestResults();
  }

  getPercents(item) {
    var percents =  Math.round(item.done * 100 / item.total)
    return `${percents} %`;
  }

  openHistoryModal() {
    const modalRef: any = this.modalService.open(AddTestHistoryModalComponent, { centered: true, size: 'lg', windowClass: 'my-class', scrollable: true });
    modalRef.result.then((response) => (response), () => { });
  }

}
