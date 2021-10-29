import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthenticationService } from '../../services/authentication.service';
// import { AuthService } from '../../services/auth.service';

@Component({
	selector: 'app-expired-session-modal',
	templateUrl: './expired-session-modal.component.html',
	styleUrls: ['./expired-session-modal.component.scss'],
})
export class ExpiredSessionModalComponent {
	@Input() title: string;
	@Input() description: string;

	constructor(private activeModal: NgbActiveModal, private authService: AuthenticationService) {}

	close(): void {
		this.activeModal.close();
		this.authService.login();
	}
}
