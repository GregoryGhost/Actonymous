import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormData } from 'src/app/shared';
import { SettingsExportReport } from '../../models';

type SettingsExportReportForm = FormData<SettingsExportReport>;

@Component({
  selector: 'settings-export-report-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {
  @Input()
  public data: SettingsExportReport | null;

  @Output()
  public onSubmit: EventEmitter<SettingsExportReport>;

  public readonly pageForm: SettingsExportReportForm;

  constructor(private readonly fb: FormBuilder) {
    //TODO: add form controls
    this.pageForm = this.fb.group({
      jiraCredentials: this.fb.control(
        { login: '', password: '', serverAddress: '' },
        { nonNullable: true, validators: [Validators.required] }
      ),
      morpher: this.fb.control(
        { accessToken: '' },
        { nonNullable: true, validators: [Validators.required] }
      ),
    });
    this.onSubmit = new EventEmitter<SettingsExportReport>();
    this.data = null;
  }

  ngOnInit(): void {}

  public submit(): void {
    if (this.pageForm.invalid) {
      return;
    }

    const data = JSON.stringify(this.pageForm.getRawValue());
    console.log(data);
  }
}
