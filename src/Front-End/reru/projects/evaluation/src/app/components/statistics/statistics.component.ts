import { Component, OnInit } from '@angular/core';
import { SelectItem } from '../../utils/models/select-item.model';
import { ReferenceService } from '../../utils/services/reference/reference.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.scss']
})
export class StatisticsComponent implements OnInit {

  statisticEnum: SelectItem[] = [{ label: "", value: "" }];
  testTemplates: SelectItem[] = [{ label: "", value: "" }];
  categories: SelectItem[] = [{ label: "", value: "" }];
  filterEnum = 3;
  testTemplateId;
  itemsPerPage;
  categoryId;
  title: string;
  
  constructor(
    private referenceService: ReferenceService
  ) { }

  ngOnInit(): void {
    this.getStatisticType();
    this.getTestTemplates();
    this.getQuestionCategories();
  }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

  getStatisticType() {
    this.referenceService.getStatisticEnum().subscribe((res) => this.statisticEnum = res.data);
  }

  getTestTemplates() {
    this.referenceService.getTestTemplates().subscribe((res) => this.testTemplates = res.data);
  }

  getQuestionCategories() {
    this.referenceService.getQuestionCategory().subscribe((res) => this.categories = res.data);
  }

  send() {
    return {
      testTemplateId: this.testTemplateId,
      categoryId: this.categoryId,
      itemsPerPage: this.itemsPerPage,
      filterEnum: this.filterEnum
    }
  }

}
