import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { EventService } from '../../../utils/services/event/event.service';
import { Location } from '@angular/common';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { Event } from '../../../utils/models/events/event.model';

@Component({
  selector: 'app-add-edit-events',
  templateUrl: './add-edit-events.component.html',
  styleUrls: ['./add-edit-events.component.scss']
})
export class AddEditEventsComponent implements OnInit {
  eventId;
	eventName;
	name;
	startDate;
	endDate;
	fromDate;
	tillDate;
	description;
	isLoading: boolean = true;

	constructor(
		private eventService: EventService,
		private location: Location,
		private activatedRoute: ActivatedRoute,
		private notificationService: NotificationsService
	) { }


	ngOnInit(): void {
		this.initData();
	}

	initData(): void {
		this.eventId = this.activatedRoute.snapshot.paramMap.get('id');
		if (this.eventId != null) this.getEvent(this.eventId)
		else this.isLoading = false;
		console.log(" this.eventID", this.eventId);
		
	}

	getEvent(id: number): void {
		this.eventService.getEvent(id).subscribe(res => {
			if (res && res.data) {
				this.name = res.data.name;
				this.description = res.data.description;
				this.startDate = res.data.fromDate;
				this.endDate = res.data.tillDate;
				this.isLoading = false;
			}
		});
		console.log("id", id);
		
	}

	onSave(): void {
		if (this.eventId) {
			this.edit();
		} else {
			this.add();
		}
	}

	setTimeToSearch(): void {
		if (this.startDate) {
			const date = new Date(this.startDate);
			this.fromDate = new Date(date.getTime() - (new Date(this.startDate).getTimezoneOffset() * 60000)).toISOString();
		}
		if (this.endDate) {
			const date = new Date(this.endDate);
			this.tillDate = new Date(date.getTime() - (new Date(this.endDate).getTimezoneOffset() * 60000)).toISOString();
		}
	}

	parse() {
		this.setTimeToSearch();
		if(this.eventId != null){
			return {
			data: new Event({
				id: this.eventId,
				name: this.name,
				fromDate: this.fromDate,
				tillDate: this.tillDate,
				description: this.description
			})
		};
		}else{
			return {
				data: new Event({
					name: this.name,
					fromDate: this.fromDate,
					tillDate: this.tillDate,
					description: this.description
				})
			};
		}
		
	}

	add(): void {
		this.eventService.addEvent(this.parse()).subscribe(() => {
			this.backClicked();
			this.notificationService.success('Success', 'Event was successfully added', NotificationUtil.getDefaultMidConfig());
		});
	}

	edit(): void {
		this.eventService.editEvent(this.parse()).subscribe(() => {
			this.backClicked();
			this.notificationService.success('Success', 'Event was successfully updated', NotificationUtil.getDefaultMidConfig());
		});
	}

	backClicked() {
		this.location.back();
	}
}
