import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'erp-shared-go-to-test-modal',
  templateUrl: './go-to-test-modal.component.html',
  styleUrls: ['./go-to-test-modal.component.scss']
})
export class GoToTestModalComponent implements OnInit {

  @Input() testData: any;

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  goToTest(id) {
		const location = window.location;
    let locationToRedirect = `${location.protocol}//${location.host}/reru-evaluation/#/my-activities/start-test/${id}`;
    window.open(locationToRedirect, '_self');
    this.dismiss();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
