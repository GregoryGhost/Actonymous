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
import { TuiButtonModule, TuiErrorModule } from '@taiga-ui/core';
import { TuiFieldErrorPipeModule } from '@taiga-ui/kit';

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
    TuiButtonModule,
    TuiErrorModule,
    TuiFieldErrorPipeModule,
  ],
})
export class SettingsExportReportModule {}
