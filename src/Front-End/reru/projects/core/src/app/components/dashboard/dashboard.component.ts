import { Component, OnInit } from '@angular/core';
import { I18nService } from '../../utils/services/i18n.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(private translate: I18nService) {}

  ngOnInit(): void {
    alert('Core works!')
  }

}
