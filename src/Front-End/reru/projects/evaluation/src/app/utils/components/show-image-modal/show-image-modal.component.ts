import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-show-image-modal',
  templateUrl: './show-image-modal.component.html',
  styleUrls: ['./show-image-modal.component.scss']
})
export class ShowImageModalComponent {
  @Input() imageUrl: string;

  constructor(
    public activeModal: NgbActiveModal,
  ) { }

  dismiss(){
		this.activeModal.close();
	}

}
