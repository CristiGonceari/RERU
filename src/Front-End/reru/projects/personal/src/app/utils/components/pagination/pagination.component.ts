import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {
  items: number[] = [];
  @Input() size: string = '10';
  @Input() page: number;
  @Input() total: number;
  @Input() position: string;
  @Output() pageChange: EventEmitter<number> = new EventEmitter<number>();
  @Output() sizeChange: EventEmitter<number> = new EventEmitter<number>();
  constructor() { }

  ngOnInit(): void {
    this.initPagination();
  }

  ngOnChanges(): void {
    this.initPagination();
  }

  initPagination(): void {
    const from = (this.page - 2) < 3 ? 1 : this.page - 2;
    const to = from + 5;
    this.items = [];
    for (let i = from; i < to; i++) {
      if (i <= this.total) {
        this.items.push(i);
      }
    }
  }

  changePage(page: number): void {
    if (this.total === this.page && page === this.total ||
      page === this.total && page === this.page ||
      this.page === page || !page || page < 1 || page > this.total) {
      return;
    }

    this.pageChange.emit(page);
  }

  changeSize(event) {
    this.sizeChange.emit(+event.target.value);
  }
}
