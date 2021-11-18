import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { UserProfileService } from 'projects/evaluation/src/app/utils/services/user-profile/user-profile.service';

@Component({
  selector: 'app-search-person',
  templateUrl: './search-person.component.html',
  styleUrls: ['./search-person.component.scss']
})
export class SearchPersonComponent implements OnInit {
  list = [];
  form = new FormControl();
  planId;
  userId;

  constructor(private planService: PlanService,
    private userService: UserProfileService,
    private activatedRoute: ActivatedRoute,
    ) { }

  ngOnInit() {
    this.getList();
  }

  initData() {
    this.planId = this.activatedRoute.snapshot.paramMap.get('id');
    this.planService.get(this.planId).subscribe(() => {
      this.form.setValue(null)
    })
  }

  getList() {
   this.initData();


    this.form.valueChanges.subscribe(term => {
      if (!term)
        this.planService.getNoAssignedPersonToPlans({ planId: this.planId }).subscribe(data => this.list = data.data);
      else if (term != '')
        this.planService.getNoAssignedPersonToPlans({ keyword: term, planId: this.planId }).subscribe(data => this.list = data.data);
      
      this.planService.setUser(term);
    });
  }

  getTitle(userId) {
    if (userId)
      return this.list.find(u => u.id === userId).firstName;
  }
}
