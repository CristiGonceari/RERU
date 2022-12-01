import { Directive, ElementRef, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { FileService } from '../services/file.service';
import { NotificationUtil } from '../util/notification.util';

@Directive({
  selector: '[appSign]'
})
export class SignDirective implements OnInit {
  @Input() id;
  @Input() name: string;
  @Output() refresh: EventEmitter<void> = new EventEmitter<void>();
  @HostListener('click', ['$event']) onClick($event): void {
    // this.openSignFileModal();
  }

  constructor(private el: ElementRef,
              private fileService: FileService,
              private modalService: NgbModal,
              private notificationService: NotificationsService) {}

  ngOnInit(): void {
    this.checkFileExtension();
  }

  // openSignFileModal(): void {
  //   const modalRef = this.modalService.open(SignFileModalComponent);
  //   modalRef.result.then((response) => this.signFile(response), () => {});
  // }

  signFile(data): void {
    const form = new FormData();
    form.append('id', `${this.id}`);
    form.append('file', data);
    this.fileService.sign(form).subscribe(() => {
      this.refresh.emit();
      this.notificationService.success('Success', 'File signed!', NotificationUtil.getDefaultConfig());
    });
  }

  checkFileExtension(): void {
    if (this.name && !this.name.trim().endsWith('.pdf')) {
      this.el.nativeElement.style.display = 'none';
    }
  }
}
