import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';
import { UserProfileService } from 'projects/evaluation/src/app/utils/services/user-profile/user-profile.service';

@Component({
  selector: 'app-search-person',
  templateUrl: './search-person.component.html',
  styleUrls: ['./search-person.component.scss']
})
export class SearchPersonComponent implements OnInit {
  list;
  form = new FormControl();
  locationId;
  userId;

  constructor(
    private userService: UserProfileService,
    private locationService: LocationService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getList();
  }

  initData() {
    this.locationId = this.activatedRoute.snapshot.paramMap.get('id');
    this.locationService.getLocation(this.locationId).subscribe(res => {
      this.form.setValue(res.data.userProfileId)
      this.getTitle(res.data.userProfileId);
    })
  }

  getList() {
    this.initData();
 
     this.form.valueChanges.subscribe(term => {
       if (!term)
         this.userService.getUserProfilesByLocation({ locationId: this.locationId }).subscribe(data => this.list = data.data);
       else if (term != '')
         this.userService.getUserProfilesByLocation({ keyword: term, locationId: this.locationId }).subscribe(data => this.list = data.data);
         
       this.locationService.setData(term);
     });
   }
 
   getTitle(userId) {
     if (userId){
      var user =  this.list.find(u => u.id === userId);

      if(user.lastName == null)
      {
        user.lastName = "";
      }

      var name = user.firstName + " " + user.lastName;

      return name;
     }
   }
}
