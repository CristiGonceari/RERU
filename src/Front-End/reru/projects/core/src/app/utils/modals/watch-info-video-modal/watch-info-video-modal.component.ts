import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-watch-info-video-modal',
  templateUrl: './watch-info-video-modal.component.html',
  styleUrls: ['./watch-info-video-modal.component.scss']
})
export class WatchInfoVideoModalComponent implements OnInit {
  familyForm: any;

  constructor(private activeModal: NgbActiveModal,
    private modalService: NgbModal,
    ) { }

  ngOnInit(): void {
  }

  close(): void {
    this.activeModal.close();
  }


  dismiss(): void {
    this.activeModal.dismiss();
  }

}
