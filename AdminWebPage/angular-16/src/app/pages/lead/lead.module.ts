import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeadRoutingModule } from './lead-routing.module';
import { LeadListComponent } from './components/lead-list/lead-list.component';
import { LeadAddComponent } from './components/lead-add/lead-add.component';
import { LeadEditComponent } from './components/lead-edit/lead-edit.component';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    LeadListComponent,
    LeadAddComponent,
    LeadEditComponent
  ],  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    LeadRoutingModule,
    MatTableModule,
    MatIconModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatSelectModule
  ]
})
export class LeadModule { }
