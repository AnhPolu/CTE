import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SettingRoutingModule } from './setting-routing.module';
import { SettingComponent } from './setting.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { UserListComponent } from './components/user-list/user-list.component';
import { LeadModule } from '../lead/lead.module';


@NgModule({
  declarations: [
    SettingComponent,
    UserListComponent
  ],
  imports: [
    CommonModule,
    SettingRoutingModule,
    LeadModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule
  ]
})
export class SettingModule { }
