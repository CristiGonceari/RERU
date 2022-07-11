import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { SolicitedVacantPositionEmailMessage } from '../../enums/solicited-vacant-position-email.enum';
import { SolicitedVacantPositionEmailMessageService } from '../../services/solicited-vacant-position-email-message/solicited-vacant-position-email-message.service';

@Component({
  selector: 'app-review-solicited-vacand-position-modal',
  templateUrl: './review-solicited-vacand-position-modal.component.html',
  styleUrls: ['./review-solicited-vacand-position-modal.component.scss']
})
export class ReviewSolicitedVacandPositionModalComponent implements OnInit {

  userEmail: string;
  userName: string;

  messageEnum = SolicitedVacantPositionEmailMessage;

  mesageType;

  active = 0;

  public Editor = DecoupledEditor;
  public onReady(editor) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  editorData: string = '';

  constructor(
    private activeModal: NgbActiveModal,
    private solicitedVacantPositionEmailMessageService: SolicitedVacantPositionEmailMessageService
  ) { }

  ngOnInit(): void {
    this.getEmailMessage(this.messageEnum.Approve)
  }

  getEmailMessage(type){
  this.mesageType = type;
  this.solicitedVacantPositionEmailMessageService.getMessage({messageType: type}).subscribe(res => {
    this.editorData = res.data
  })
  }

  close(): void {
    let data = {
      messageEnum: this.mesageType,
      EmailMessage: this.editorData
    }

    this.activeModal.close(data);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }


}
