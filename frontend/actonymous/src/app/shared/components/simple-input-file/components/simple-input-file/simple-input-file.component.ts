import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  forwardRef,
  OnInit,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormBuilder,
  FormControl,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
} from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { TuiFileLike } from '@taiga-ui/kit/interfaces';
import {
  requiredInputMsg,
  maxLengthValidator,
  minLengthValidator,
} from 'src/app/shared/utils';

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
    {
      provide: TUI_VALIDATION_ERRORS,
      useValue: {
        required: requiredInputMsg,
        maxlength: maxLengthValidator,
        minlength: minLengthValidator,
      },
    },
    {
      provide: NG_VALIDATORS,
      useExisting: SimpleInputFileComponent,
      multi: true,
    },
  ],
})
export class SimpleInputFileComponent implements OnInit, ControlValueAccessor {
  public readonly fileControl: FormControl<File>;

  public required = false;

  constructor(
    private readonly fb: FormBuilder,
    private readonly cd: ChangeDetectorRef
  ) {
    this.fileControl = fb.control(null);
  }

  ngOnInit(): void {}

  writeValue(value: File): void {
    this.fileControl.setValue(value);
  }

  registerOnChange(fn: CallbackFileValue): void {
    this.fileControl.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((value) => {
        fn(value);
        this.cd.markForCheck();
      });
  }

  registerOnTouched(fn: CallbackFileValue): void {}

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.fileControl.disable();
    } else {
      this.fileControl.enable();
    }
  }

  validate(control: AbstractControl): ValidationErrors {
    const haveNoErrors = null as unknown as ValidationErrors;
    if (!(control && control.validator)) {
      return haveNoErrors;
    }
    const emptyArg = {} as AbstractControl;
    this.required = control.validator(emptyArg)?.['required'];
    this.cd.markForCheck();

    return haveNoErrors;
  }

  public removeFile(): void {
    this.fileControl.setValue(null);
    this.cd.markForCheck();
  }
}
