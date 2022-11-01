import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TuiFileLike, TuiFileState } from '@taiga-ui/kit';

type TemplateBindingForm = {
  actTemplateFile: FormControl,
  taskTemplateFile: FormControl
};
type TemplateBindingFormGroup = FormGroup<TemplateBindingForm>;

@Component({
  selector: 'template-bindings',
  templateUrl: './template-bindings.component.html',
  styleUrls: ['./template-bindings.component.scss']
})
export class TemplateBindingsComponent implements OnInit {

  @Input()
  public actTemplateFileState: TuiFileState;

  @Input()
  public actTemplateFile: TuiFileLike | undefined;

  @Input()
  public taskTemplateFileState: TuiFileState;

  @Input()
  public taskTemplateFile: TuiFileLike | undefined;

  public form: TemplateBindingFormGroup;

  constructor(private readonly fb: FormBuilder) {
    this.actTemplateFileState = 'normal';
    this.actTemplateFile = undefined;
    this.taskTemplateFileState = 'normal';
    this.taskTemplateFile = undefined;
    this.form = this.fb.group({
      actTemplateFile: [undefined, [Validators.required]],
      taskTemplateFile: [undefined, [Validators.required]],
    });
  }

  ngOnInit(): void {
  }

  public removeActTemplateFile(): void {
    //TODO:
  }

  public removeTaskTemplateFile(): void {
    //TODO:
  }

  public onRejectActTemplateFile(event: TuiFileLike | readonly TuiFileLike[]): void {
    //TODO:
  }

  public onRejectTaskTemplateFile(event: TuiFileLike | readonly TuiFileLike[]): void {
    //TODO:
  }
}
