import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  TuiButtonModule,
  TuiErrorModule,
  TuiLabelModule
} from '@taiga-ui/core';
import {
  TextMaskModule,
  TuiFieldErrorPipeModule,
  TuiInputDateModule, TuiInputModule,
  TuiInputPasswordModule
} from '@taiga-ui/kit';
import { SharedModule, SimpleInputFileModule } from 'src/app/shared';
import {
  ContractInfoComponent,
  CustomerInfoComponent,
  ExecutorInfoComponent,
  FormComponent,
  JiraCredentialsComponent,
  MorpherComponent,
  TemplateBindingsComponent
} from './components/';
import { PageComponent } from './containers/';
import { SettingsExportReportRoutingModule } from './settings-export-report-routing.module';

const COMPONENTS = [
  FormComponent,
  TemplateBindingsComponent,
  JiraCredentialsComponent,
  MorpherComponent,
  CustomerInfoComponent,
  ExecutorInfoComponent,
  ContractInfoComponent,
];
const CONTAINERS = [PageComponent];

@NgModule({
  declarations: [COMPONENTS, CONTAINERS],
  imports: [
    CommonModule,
    SettingsExportReportRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    TuiButtonModule,
    TuiErrorModule,
    TuiFieldErrorPipeModule,
    TuiLabelModule,
    TuiInputModule,
    SimpleInputFileModule,
    TuiInputPasswordModule,
    TextMaskModule,
    TuiInputDateModule,
  ],
})
export class SettingsExportReportModule {}
