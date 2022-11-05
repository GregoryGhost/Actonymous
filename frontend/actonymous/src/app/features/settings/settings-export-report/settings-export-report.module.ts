import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SettingsExportReportRoutingModule } from './settings-export-report-routing.module';
import { PageComponent } from './containers/';
import {
  FormComponent,
  JiraCredentialsComponent,
  MorpherComponent,
  TemplateBindingsComponent,
} from './components/';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  TuiButtonModule,
  TuiErrorModule,
  TuiLabelModule,
} from '@taiga-ui/core';
import {
  TextMaskModule,
  TuiFieldErrorPipeModule,
  TuiInputFilesModule,
  TuiInputModule,
  TuiInputPasswordModule,
} from '@taiga-ui/kit';
import { SharedModule } from 'src/app/shared';

const COMPONENTS = [
  FormComponent,
  TemplateBindingsComponent,
  JiraCredentialsComponent,
  MorpherComponent,
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
    TuiInputFilesModule,
    TuiInputPasswordModule,
    TextMaskModule
  ],
})
export class SettingsExportReportModule {}
