import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportGenerationRoutingModule } from './report-generation-routing.module';
import { PageComponent } from './containers/page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TuiFieldErrorPipeModule, TuiInputDateRangeModule } from '@taiga-ui/kit';
import { TuiButtonModule, TuiErrorModule } from '@taiga-ui/core';


@NgModule({
  declarations: [
    PageComponent
  ],
  imports: [
    CommonModule,
    ReportGenerationRoutingModule,
    TuiInputDateRangeModule,
    FormsModule,
    ReactiveFormsModule,
    TuiButtonModule,
    TuiErrorModule,
    TuiFieldErrorPipeModule
  ]
})
export class ReportGenerationModule { }
