import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-details-modal',
  templateUrl: './details-modal.component.html',
  styleUrls: ['./details-modal.component.scss']
})
export class DetailsModalComponent implements OnInit {

  @Input() id;
  @Input() items;

  item;
  itemIndex: number;
  json;
  itemIndexLength;

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    
    this.selectedValue(this.id);
    this.itemIndexLength = this.items.length - 1;
  }
 
  selectedValue(id: number){
    const value = this.items.findIndex(x => x.id === id)
    this.itemIndex = value;
    
    this.values(value);
  }

  dismiss(): void {
		this.activeModal.close();
	}

  increaseModal(){
    this.itemIndex ++

    if (this.itemIndex <= this.itemIndexLength && this.itemIndex >= 0){
      this.values(this.itemIndex)
    }
    else{
      this.itemIndex --;
    }
  }

  decreaseModal(){
    this.itemIndex --;

    if (this.itemIndex <= this.itemIndexLength && this.itemIndex >= 0){
      this.values(this.itemIndex)
    }
    else{
      this.itemIndex ++;
    }
  }

  values(index){
     this.item = this.items[index]

     if(this.item.jsonMessage != ""){
        this.json = JSON.stringify(JSON.parse(this.item.jsonMessage), null, 3);
     }
     else{
       this.json = null;
     }
    }
  }
