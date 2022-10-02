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
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
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

  openHistoryModal() {
    const modalRef: any = this.modalService.open(AddTestHistoryModalComponent, { centered: true, size: 'lg', windowClass: 'my-class', scrollable: true });
    modalRef.result.then((response) => (response), () => { });
  }

}
