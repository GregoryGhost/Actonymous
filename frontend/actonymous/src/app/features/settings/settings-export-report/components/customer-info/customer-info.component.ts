import {
  ChangeDetectionStrategy,
  Component,
  forwardRef,
  OnInit,
} from '@angular/core';
import {
  NG_VALUE_ACCESSOR,
  NG_VALIDATORS,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { UntilDestroy } from '@ngneat/until-destroy';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import {
  requiredInputMsg,
  maxLengthValidator,
  minLengthValidator,
  BaseFormGroup,
} from 'src/app/shared';
import { FormData } from 'src/app/shared';
import { CustomerInfo } from '../../models';

type CustomerInfoForm = FormData<CustomerInfo>;

@UntilDestroy()
@Component({
  selector: 'customer-info',
  templateUrl: './customer-info.component.html',
  styleUrls: ['./customer-info.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CustomerInfoComponent),
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
      useExisting: forwardRef(() => CustomerInfoComponent),
      multi: true,
    },
  ],
})
export class CustomerInfoComponent
  extends BaseFormGroup<CustomerInfo>
  implements OnInit
{
  constructor(private readonly fb: FormBuilder) {
    super(CustomerInfoComponent.initForm(fb));
  }

  ngOnInit(): void {}

  private static initForm(fb: FormBuilder): CustomerInfoForm {
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
    });
  }
}
