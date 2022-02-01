import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-view-json-modal',
  templateUrl: './view-json-modal.component.html',
  styleUrls: ['./view-json-modal.component.scss']
})
export class ViewJsonModalComponent implements OnInit {
  @Input() json;

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.json = JSON.stringify(JSON.parse(this.json), null, 3);
  }

  dismiss(): void {
		this.activeModal.close();
	}
}
