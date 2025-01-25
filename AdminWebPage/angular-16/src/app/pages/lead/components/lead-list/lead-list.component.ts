import { Component } from '@angular/core';
import { ILead } from 'src/app/models/ILead';

@Component({
  selector: 'app-lead-list',
  templateUrl: './lead-list.component.html',
  styleUrls: ['./lead-list.component.scss'] 
})
export class LeadListComponent {
  leadList : ILead [] = [
    {
      name: 'John Smith',
      dob: new Date('1990-05-23'),
      phone: '123-456-7890',
      level: 1,
      note: 'New lead'
    },
    {
      name: 'Kate Brown',
      dob: new Date('1995-02-11'),
      phone: '234-567-8901',
      level: 2,
      note: 'Old lead'
    },
    {
      name: 'Bob Johnson',
      dob: new Date('1980-08-25'),
      phone: '345-678-9012',
      level: 3,
      note: 'Potential customer'
    },
    {
      name: 'Alice Martin',
      dob: new Date('1975-03-10'),
      phone: '456-789-0123',
      level: 4,
      note: 'Best customer'
    },
    {
      name: 'Robert Davis',
      dob: new Date('1960-01-01'),
      phone: '567-890-1234',
      level: 5,
      note: 'Most profitable customer'
    },
  ];

  displayColumns: string[] = ['name', 'dob', 'phone', 'level', 'note', 'actions'];

  constructor() {
  }

  onAddClick() {
    const newLead: ILead = {
      name: '',
      dob: new Date(),
      phone: '',
      level: 1,
      note: ''
    };
    this.leadList.push(
      
    );
  }

  onEditClick(lead: ILead) {  
    console.log(lead);
  }

  onDeleteClick(lead: ILead) {
    console.log(lead);
  } 
}
