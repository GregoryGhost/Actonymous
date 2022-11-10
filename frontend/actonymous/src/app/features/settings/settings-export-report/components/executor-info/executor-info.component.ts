import {
  ChangeDetectionStrategy,
  Component,
  forwardRef,
  OnInit,
} from '@angular/core';
import {
  FormBuilder,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  Validators,
} from '@angular/forms';
import { UntilDestroy } from '@ngneat/until-destroy';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import {
  BaseFormGroup,
  FormData,
  maxLengthValidator,
  maxValueValidator,
  minLengthValidator,
  minValueValidator,
  requiredInputMsg,
} from 'src/app/shared';
import { ExecutorInfo } from '../../models';

type ExecutorInfoForm = FormData<ExecutorInfo>;

@UntilDestroy()
@Component({
  selector: 'executor-info',
  templateUrl: './executor-info.component.html',
  styleUrls: ['./executor-info.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ExecutorInfoComponent),
      multi: true,
    },
    {
      provide: TUI_VALIDATION_ERRORS,
      useValue: {
        required: requiredInputMsg,
        maxlength: maxLengthValidator,
        minlength: minLengthValidator,
        min: minValueValidator,
        max: maxValueValidator,
      },
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => ExecutorInfoComponent),
      multi: true,
    },
  ],
})
export class ExecutorInfoComponent
  extends BaseFormGroup<ExecutorInfo>
  implements OnInit
{
  constructor(private readonly fb: FormBuilder) {
    super(ExecutorInfoComponent.initForm(fb));
  }

  ngOnInit(): void {}

  private static initForm(fb: FormBuilder): ExecutorInfoForm {
    return fb.nonNullable.group({
      companyName: fb.nonNullable.control('', {
        validators: [Validators.required, Validators.maxLength(250)],
      }),
      headerFullname: fb.nonNullable.control('', {
        validators: [Validators.required, Validators.maxLength(250)],
      }),
      headerPosition: fb.nonNullable.control('', {
        validators: [Validators.required, Validators.maxLength(250)],
      }),
      ratePerHour: fb.nonNullable.control(0, {
        validators: [
          Validators.required,
          Validators.min(1),
          Validators.max(999_999_999),
        ],
      }),
    });
  }
}
