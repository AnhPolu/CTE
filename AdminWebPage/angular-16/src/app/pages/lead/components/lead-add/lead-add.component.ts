import { Component } from '@angular/core';
import { ILead } from 'src/app/models/ILead';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-lead-add',
  templateUrl: './lead-add.component.html',
  styleUrls: ['./lead-add.component.scss']
})
export class LeadAddComponent {
  newLead = new FormGroup({
    name: new FormControl(''),
    dob: new FormControl(new Date()),
    phone: new FormControl(''),
    level: new FormControl(1),
    note: new FormControl('')
  });
}
