import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { CandidatePositionModel } from '../../utils/models/candidate-position.model';
import { PaginationSummary } from '../../utils/models/pagination-summary.model';
import { CandidatePositionService } from '../../utils/services/candidate-position.service';
import { I18nService } from '../../utils/services/i18n.service';
import { NotificationUtil } from '../../utils/util/notification.util';

@Component({
  selector: 'app-positions',
  templateUrl: './positions.component.html',
  styleUrls: ['./positions.component.scss']
})
export class PositionsComponent implements OnInit {
  isLoading = true;
	positions: CandidatePositionModel[];
	pagination: PaginationSummary = new PaginationSummary();
	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(
		private positionService: CandidatePositionService,
		public translate: I18nService,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
	) {}

  ngOnInit(): void {
    this.getPositions();
  }

  getPositions(data: any = {}): void {
		let params: any = {
      name: data.keyword || '',
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		};
		this.list(params);
	}
	
	list(params): void {
		this.positionService.getList(params).subscribe(res => {
			if(res && res.data) {
				this.positions = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

  openRemoveModal(id: number, name: string): void {
		forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('pages.modules.delete-msg'),
			this.translate.get('modal.no'),
			this.translate.get('modal.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = `${this.description} (${name})?`;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.removePosition(id, name), () => {});
	}

	removePosition(id: number, name: string): void {
		this.positionService.delete(id).subscribe(res => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('modules.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title,`${name} ${this.description}`, NotificationUtil.getDefaultMidConfig(),
		 			this.getPositions()
				);
			},
			() => {
				this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

}
