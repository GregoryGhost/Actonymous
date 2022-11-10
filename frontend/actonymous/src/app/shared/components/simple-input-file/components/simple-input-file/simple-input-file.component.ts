import { ChangeDetectionStrategy, Component, forwardRef, OnInit } from '@angular/core';
import { ControlValueAccessor, FormBuilder, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { TuiFileLike } from '@taiga-ui/kit/interfaces';

type File = TuiFileLike | null;
type CallbackFileValue = (value: File) => void;

@UntilDestroy()
@Component({
  selector: 'simple-input-file',
  templateUrl: './simple-input-file.component.html',
  styleUrls: ['./simple-input-file.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SimpleInputFileComponent),
      multi: true,
    },
  ],
})
export class SimpleInputFileComponent implements OnInit, ControlValueAccessor {
  public readonly fileControl: FormControl<File>;

  constructor(private readonly fb: FormBuilder) {
    this.fileControl = fb.control(null);
  }

  ngOnInit(): void {}

  writeValue(value: File): void {
    this.fileControl.setValue(value);
  }

  registerOnChange(fn: CallbackFileValue): void {
    this.fileControl.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe(fn as any);
  }

  registerOnTouched(fn: CallbackFileValue): void {}

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.fileControl.disable();
    } else {
      this.fileControl.enable();
    }
  }

  public removeFile(): void {
    this.fileControl.setValue(null);
  }
}
