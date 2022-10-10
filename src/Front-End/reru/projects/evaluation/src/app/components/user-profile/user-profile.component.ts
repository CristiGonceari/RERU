import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../utils/services/i18n/i18n.service';
import { UserProfileService } from '../../utils/services/user-profile/user-profile.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  acronym: string;
  isLoading = false;
  user;

  constructor(
    private activatedRoute: ActivatedRoute,
		private modalService: NgbModal,
		public translate: I18nService,
    private userService: UserProfileService,
		public router: Router
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.isLoading = true;
    this.activatedRoute.params.subscribe(params => {
      if (params.id) {
        this.getUserById(params.id);
      }
    });
  }

  getUserById(id: number) {
		this.userService.getUser(id).subscribe(response => {
      this.user = response.data;
			this.subscribeForUserChanges(response.data);
			this.isLoading = false;
		});
	}

  subscribeForUserChanges(user): void {
    let matches = user && (user.lastName + ' ' + user.firstName).match(/\b(\w)/g);
    if(user.lastName == null){
      matches = user && (user.firstName).match(/\b(\w)/g);
    }
    this.acronym = matches ? matches.join('') : null;
	}

  openConfirmationModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });

    forkJoin([
			this.translate.get('user-profile.navigate-core-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
		]).subscribe(([description, no, yes]) => {
      modalRef.componentInstance.description = description;
      modalRef.componentInstance.buttonNo = no;
      modalRef.componentInstance.buttonYes = yes;
			});

		modalRef.result.then(() => this.navigateToCore(id), () => { });
	}

  navigateToCore(id): void {
    let host = window.location.host;
		window.open(`http://${host}/#/user-profile/${id}/overview`, '_self')
  }
}
