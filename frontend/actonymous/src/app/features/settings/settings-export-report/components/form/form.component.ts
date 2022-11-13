import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TuiFileLike } from '@taiga-ui/kit';
import { MainForm } from 'src/app/shared';
import {
  JiraCredentials,
  MorpherInfo,
  SettingsExportReport,
  TemplateBindingsInfo,
} from '../../models';

type SettingsExportReportForm = MainForm<SettingsExportReport>;

@Component({
  selector: 'settings-export-report-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormComponent implements OnInit {
  @Input()
  public data: SettingsExportReport | null;

  @Output()
  public onSubmit: EventEmitter<SettingsExportReport>;

  public readonly pageForm: SettingsExportReportForm;

  constructor(private readonly fb: FormBuilder) {
    this.pageForm = FormComponent.initForm(fb);
    this.onSubmit = new EventEmitter<SettingsExportReport>();
    this.data = null;
  }

  ngOnInit(): void {}

  public submit(): void {
    this.pageForm.markAllAsTouched();
    if (this.pageForm.invalid) {
      return;
    }

    this.onSubmit.emit(this.pageForm.getRawValue());
  }

  private static initForm(fb: FormBuilder): SettingsExportReportForm {
    const emptyFile = null as TuiFileLike | null;
    const templateBindingsInfo: TemplateBindingsInfo = {
      customerInfo: {
        companyName: '',
        headerFullname: '',
        headerPosition: '',
      },
      executorInfo: {
        companyName: '',
        headerFullname: '',
        headerPosition: '',
        ratePerHour: 0,
      },
      contractInfo: {
        contractNumber: '',
        approvalDate: new Date(),
        contractFile: emptyFile,
      },
      actTemplateFile: emptyFile,
      taskTemplateFile: emptyFile,
    };
    const jiraCredentials: JiraCredentials = {
      login: '',
      password: '',
      serverAddress: '',
    };
    const morpher: MorpherInfo = { accessToken: '' };

    return fb.nonNullable.group({
      jiraCredentials: fb.nonNullable.control(jiraCredentials, {
        validators: [Validators.required],
      }),
      morpher: fb.nonNullable.control(morpher, {
        validators: [Validators.required],
      }),
      templateBindings: fb.nonNullable.control(templateBindingsInfo, {
        validators: [Validators.required],
      }),
    });
  }
}
