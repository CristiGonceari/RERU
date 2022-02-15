import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';
import { UserProfileService } from 'projects/evaluation/src/app/utils/services/user-profile/user-profile.service';
import { EventTestTypeService } from 'projects/evaluation/src/app/utils/services/event-test-type/event-test-type.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  @Input() searchLocations: boolean;
  @Input() searchEvaluators: boolean;
  @Input() searchTestType: boolean;
  @Input() searchPerson: boolean;
  @Input() searchUser: boolean;

  list;
  form = new FormControl();
  eventId: number;
  id: number;

  constructor(private locationService: LocationService,
    private eventService: EventService,
    private eventTestTypeService: EventTestTypeService,
    private activatedRoute: ActivatedRoute,
    private userService: UserProfileService,
    private testService: TestTypeService,) { }

  ngOnInit() {
    this.getList();
  }

  initData() {
    this.eventId = +this.activatedRoute.snapshot.paramMap.get('id');
    this.eventService.getEvent(this.eventId).subscribe(res => {
      if (this.searchLocations == false) {
        this.form.setValue(res.data.locationId)
        this.getTitle(res.data.locationId);
      }

      if (this.searchEvaluators == false) {
        this.form.setValue(res.data.evaluatorId)
        this.getTitle(res.data.evaluatorId);
      }

      if (this.searchTestType == false) {
        this.form.setValue(res.data.testTypeId)
        this.getTitle(res.data.testTypeId);
      }

      if (this.searchPerson == false) {
        this.form.setValue(res.data.userProfileId)
        this.getTitle(res.data.userProfileId);
      }

      if (this.searchUser == false) {
        this.form.setValue(null)
      }
    })
  }

  getList() {
    this.initData();
    this.form.valueChanges.subscribe(term => {

      if (this.searchLocations == false) {
        if (!term) {
          this.locationService.getLocationsByEvent({ eventId: this.eventId }).subscribe(data => this.list = data.data);
        }
        else if (term != '') {
          this.locationService.getLocationsByEvent({ keyword: term, eventId: this.eventId }).subscribe(data => this.list = data.data);
        }
      }

      if (this.searchEvaluators == false) {
        if (!term)
          this.userService.getUserProfilesByEvaluatorEvent({ eventId: this.eventId }).subscribe(data => this.list = data.data);
        else if (term != '')
          this.userService.getUserProfilesByEvaluatorEvent({ keyword: term, eventId: this.eventId }).subscribe(data => this.list = data.data);
      }

      if (this.searchTestType == false) {
        if (!term)
          this.eventTestTypeService.getTestTypeByEvent({ eventId: this.eventId }).subscribe(data => this.list = data.data);
        else if (term != '')
          this.eventTestTypeService.getTestTypeByEvent({ keyword: term, eventId: this.eventId }).subscribe(data => this.list = data.data);
      }

      if (this.searchPerson == false) {
        if (!term)
          this.userService.getUserProfilesByEvent({ eventId: this.eventId }).subscribe(res => this.list = res.data);
        else if (term != '')
          this.userService.getUserProfilesByEvent({ keyword: term, eventId: this.eventId }).subscribe(data => this.list = data.data);
      }

      if (this.searchUser == false) {
        if (!term)
          this.userService.getUserProfilesByAttachedUserEvent({ eventId: this.eventId }).subscribe(data => this.list = data.data);
        else if (term != '')
          this.userService.getUserProfilesByAttachedUserEvent({ keyword: term, eventId: this.eventId }).subscribe(data => this.list = data.data);
      }
      this.eventService.setData(term);
    });


  }

  getTitle(id) {

    if (this.searchLocations == false && id) {
      return this.list.find(u => u.id === id).name + ", " + this.list.find(u => u.id === id).address;
    }

    if (this.searchTestType == false && id) {
      return this.list.find(u => u.id === id).name;
    }

    if ((this.searchPerson == false || this.searchEvaluators == false || this.searchUser == false) && id) {
      var user = this.list.find(u => u.id === id);

      if (user.lastName == null) {
        user.lastName = "";
      }

      var name = user.firstName + " " + user.lastName + ", " + user.idnp;

      return name;
    }
  }
}
