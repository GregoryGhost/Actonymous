import {
  ChangeDetectionStrategy,
  Component,
  forwardRef,
  OnInit,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  Validators,
} from '@angular/forms';
import {
  BaseFormGroup,
  maxLengthValidator,
  minLengthValidator,
  requiredInputMsg,
} from 'src/app/shared';
import { ContractInfo } from '../../models';
import { FormData } from 'src/app/shared';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { UntilDestroy } from '@ngneat/until-destroy';
import { TuiFileLike } from '@taiga-ui/kit';
import { TuiValidationError } from '@taiga-ui/cdk';

type ContractInfoForm = FormData<ContractInfo>;

@UntilDestroy()
@Component({
  selector: 'contract-info',
  templateUrl: './contract-info.component.html',
  styleUrls: ['./contract-info.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ContractInfoComponent),
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
      useExisting: forwardRef(() => ContractInfoComponent),
      multi: true,
    },
  ],
})
export class ContractInfoComponent
  extends BaseFormGroup<ContractInfo>
  implements OnInit
{
  public readonly computedError = new TuiValidationError(`An error`);
  constructor(private readonly fb: FormBuilder) {
    super(ContractInfoComponent.initForm(fb));
  }

  ngOnInit(): void {}

  private static initForm(fb: FormBuilder): ContractInfoForm {
    const contractFile = fb.control<TuiFileLike | null>(null, {
      validators: [Validators.required],
    });

    return fb.nonNullable.group({
      contractNumber: fb.nonNullable.control('', {
        validators: [Validators.required, Validators.maxLength(10)],
      }),
      approvalDate: fb.nonNullable.control(new Date(), {
        validators: [Validators.required],
      }),
      contractFile,
    });
  }
}
