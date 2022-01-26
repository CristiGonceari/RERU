import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ContractModel, InstructionModel } from 'projects/personal/src/app/utils/models/contract.model';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ContractService } from 'projects/personal/src/app/utils/services/contract.service';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import {DeleteInstructionModalComponent} from 'projects/personal/src/app/utils/modals/delete-instruction-modal/delete-instruction-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {InstructionService} from 'projects/personal/src/app/utils/services/instruction.service';
import {AddInstructionModalComponent} from 'projects/personal/src/app/utils/modals/add-instruction-modal/add-instruction-modal.component';
import { ContractorParser } from '../../add/add.parser';

@Component({
  selector: 'app-cim',
  templateUrl: './cim.component.html',
  styleUrls: ['./cim.component.scss']
})
export class CimComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @ViewChild('instance1', {static: true}) instance1: NgbTypeahead;
  @Input() contractor: Contractor;
  isLoading: boolean = true;
  individualContractForm: FormGroup;
  selectedSuperior: SelectItem;
  selectedCurrency: SelectItem;
  superiors: SelectItem[];
  currencyTypes:SelectItem[];
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  focus1$ = new Subject<string>();
  click1$ = new Subject<string>();
  contract: ContractModel;
  constructor(private fb: FormBuilder,
              private modalService: NgbModal,
              private reference: ReferenceService,
              private contractService: ContractService,
              private instructionService: InstructionService,
              private notificationService: NotificationsService) {}

  ngOnInit(): void {
    this.retrieveDropdowns();
  }

  retrieveDropdowns(): void {
    this.reference.listNomenclatureRecords({ nomenclaturebaseType: 2 }).subscribe(response => {
      this.currencyTypes = response.data;
      this.retrieveContractors();
    });
  }

  retrieveContract(): void {
    this.contractService.get(this.contractor.id).subscribe(response => {
      this.initForm(response.data);
      this.contract = {...response.data};
      this.selectedCurrency = this.currencyTypes.find(el => +el.value === this.contract.currencyTypeId)
      this.selectedSuperior = this.superiors.find(el => +el.value === this.contract.superiorId);
      this.isLoading = false;
    });
  }

  initForm(data: ContractModel): void {
    this.individualContractForm = this.fb.group({
      id: this.fb.control(data.id),
      no: this.fb.control({ value: data.no || '-', disabled: true }),
      superiorId: this.fb.control(data.superiorId),
      netSalary: this.fb.control(data.netSalary),
      brutSalary: this.fb.control(data.brutSalary),
      vacationDays: this.fb.control(data.vacationDays),
      currencyTypeId: this.fb.control(data.currencyTypeId),
      contractorId: this.fb.control(data.contractorId),
      instructions: this.fb.array(this.buildInstructions(data.instructions))
    });
  }

  buildInstructions(instructions: InstructionModel[]): FormGroup[] {
    if (!instructions || !instructions.length) {
      return [];
    }

    return instructions.map(el => this.buildInstruction(el));
  }

  buildInstruction(data: InstructionModel): FormGroup {
    return this.fb.group({
      id: this.fb.control({ value: data.id, disabled: true }),
      thematic: this.fb.control({ value: data.thematic, disabled: true }, []),
      instructorName: this.fb.control({ value: data.instructorName, disabled: true }, []),
      instructorLastName: this.fb.control({ value: data.instructorLastName, disabled: true }, []),
      duration: this.fb.control({ value: data.duration, disabled: true }, []),
      date: this.fb.control({ value: data.date, disabled: true }, []),
      contractorId: this.fb.control({value: data.contractorId, disabled: true }, [])
    });
  }

  retrieveContractors(): void {
    this.reference.listAllContractors().subscribe(response => {
      this.superiors = response.data;
      this.retrieveContract();
    })
  }

  formatter = (x:SelectItem)=> x.label;

  selectSuperior (event:SelectItem ){
    if (event) {
     this.individualContractForm.get('superiorId').patchValue(event.value);
    }
  }

  searchSuperior: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.superiors
        : this.superiors.filter(v =>v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)))
    );
  }

  searchCurrencyType: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click1$.pipe(filter(() => this.instance1 && !this.instance1.isPopupOpen()));
    const inputFocus$ = this.focus1$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.currencyTypes
        : this.currencyTypes.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  }

  selectCurrencyType(currencyType: SelectItem): void {
    if(currencyType){
      this.individualContractForm.get('currencyTypeId').patchValue(currencyType.value);
    }

  }
  

  submit(): void {
    this.isLoading = true;
    const request = this.parseCim(this.individualContractForm.getRawValue());
    this.contractService.update(request).subscribe(() => {
      this.isLoading = false;
      this.notificationService.success('Success', 'CIM updated!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
    });
  }

  parseCim(data: ContractModel): ContractModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      no: +data.no,
      superiorId: +data.superiorId,
      netSalary: +data.netSalary,
      brutSalary: +data.brutSalary,
      currencyTypeId: +data.currencyTypeId,
      contractorId: +data.contractorId
    })
  }

  resetCim(): void {
    this.initForm(this.contract);
    
  }

  openDeleteInstructionModal(id: number): void {
    const modalRef = this.modalService.open(DeleteInstructionModalComponent, { centered: true, backdrop: 'static'});
    modalRef.result.then(() => this.deleteInstruction(id), () => {});
  }

  deleteInstruction(id: number): void {
    this.isLoading = true;
    this.instructionService.delete(id).subscribe(response => {
      this.retrieveContract();
      this.notificationService.success('Success', 'Instruction deleted!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
      this.notificationService.error('Error', 'Error occured!', NotificationUtil.getDefaultConfig());
    });
  }

  openAddInstructionModal(): void {
    const modalRef = this.modalService.open(AddInstructionModalComponent, { centered: true, backdrop: 'static', size: 'lg'});
    modalRef.componentInstance.instruction = <InstructionModel>{ contractorId: this.contractor.id };
    modalRef.result.then((response) => this.addInstruction(response), () => {});
  }

  addInstruction(data: InstructionModel): void {
    this.isLoading = true;
    this.instructionService.add(ContractorParser.parseInstruction(data)).subscribe(response => {
      this.retrieveContract();
      this.notificationService.success('Success', 'Instruction added!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
      this.notificationService.error('Error', 'Error occured!', NotificationUtil.getDefaultConfig());
    });
  }

}
