import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SettingsExportReport } from '../../models';

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

  public readonly pageForm: FormGroup;

  constructor(private readonly fb: FormBuilder) {
    //TODO: add form controls
    this.pageForm = this.fb.group({
      reportPeriod: [null, [Validators.required]],
    });
    this.onSubmit = new EventEmitter<SettingsExportReport>();
    this.data = null;
  }

  ngOnInit(): void {}

  public submit(): void {
    //TODO: implement
  }
}
