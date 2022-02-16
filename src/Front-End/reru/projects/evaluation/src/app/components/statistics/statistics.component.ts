import { AfterViewInit, Component, OnInit } from '@angular/core';
import { SelectItem } from '../../utils/models/select-item.model';
import { ReferenceService } from '../../utils/services/reference/reference.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.scss']
})
export class StatisticsComponent implements OnInit, AfterViewInit {

  statisticEnum: SelectItem[] = [{ label: "", value: "" }];
  testTypes: SelectItem[] = [{ label: "", value: "" }];
  categories: SelectItem[] = [{ label: "", value: "" }];
  filterEnum = 3;
  testTypeId;
  itemsPerPage;
  categoryId;
  title: string;
  
  constructor(
    private referenceService: ReferenceService
  ) { }

  ngOnInit(): void {
    this.getStatisticType();
    this.getTestTypes();
    this.getQuestionCategories();
  }

  ngAfterViewInit(): void {
    this.title = document.getElementById('title').innerHTML;
  }

  getStatisticType() {
    this.referenceService.getStatisticEnum().subscribe((res) => this.statisticEnum = res.data);
  }

  getTestTypes() {
    this.referenceService.getTestTypes().subscribe((res) => this.testTypes = res.data);
  }

  getQuestionCategories() {
    this.referenceService.getQuestionCategory().subscribe((res) => this.categories = res.data);
  }

  send() {
    return {
      testTypeId: this.testTypeId,
      categoryId: this.categoryId,
      itemsPerPage: this.itemsPerPage,
      filterEnum: this.filterEnum
    }
  }

}
