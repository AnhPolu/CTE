import { Component, Input } from '@angular/core';
import { ILead } from 'src/app/models/ILead';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-lead-edit',
  templateUrl: './lead-edit.component.html',
  styleUrls: ['./lead-edit.component.scss']
})
export class LeadEditComponent {
  @Input() lead!: ILead;
  leadForm = new FormGroup({
    name: new FormControl(this.lead.name),
    dob: new FormControl(this.lead.dob),
    phone: new FormControl(this.lead.phone),
    level: new FormControl(this.lead.level),
    note: new FormControl(this.lead.note)
  });

}

