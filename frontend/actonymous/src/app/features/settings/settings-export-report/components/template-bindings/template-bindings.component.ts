import {
  ChangeDetectionStrategy,
  Component,
  forwardRef, OnInit
} from '@angular/core';
import {
  FormBuilder,
  FormControl, NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  Validators
} from '@angular/forms';
import { UntilDestroy } from '@ngneat/until-destroy';
import {
  TuiFileLike, TUI_VALIDATION_ERRORS
} from '@taiga-ui/kit';
import {
  BaseMainFormGroup,
  MainForm,
  maxLengthValidator,
  minLengthValidator,
  requiredInputMsg
} from 'src/app/shared';
import { ContractInfo, TemplateBindingsInfo } from '../../models';

type TemplateBindingsForm = MainForm<TemplateBindingsInfo>;

@UntilDestroy()
@Component({
  selector: 'template-bindings',
  templateUrl: './template-bindings.component.html',
  styleUrls: ['./template-bindings.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TemplateBindingsComponent),
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
      useExisting: forwardRef(() => TemplateBindingsComponent),
      multi: true,
    },
  ],
})
export class TemplateBindingsComponent
  extends BaseMainFormGroup<TemplateBindingsInfo>
  implements OnInit
{
  constructor(private readonly fb: FormBuilder) {
    super(TemplateBindingsComponent.initForm(fb));
  }

  ngOnInit(): void {}

  private static initForm(fb: FormBuilder): TemplateBindingsForm {
    const customerInfo = fb.nonNullable.control(
      {
        companyName: '',
        headerFullname: '',
        headerPosition: '',
      },
      {
        validators: [Validators.required],
      }
    );
    const executorInfo = fb.nonNullable.control(
      {
        companyName: '',
        headerFullname: '',
        headerPosition: '',
        ratePerHour: 0,
      },
      {
        validators: [Validators.required],
      }
    );
    const contractInfo: FormControl<ContractInfo> = fb.nonNullable.control(
      {
        contractNumber: '',
        approvalDate: new Date(),
        contractFile: null,
      },
      {
        validators: [Validators.required],
      }
    );
    const actTemplateFile = fb.control<TuiFileLike | null>(null, {
      validators: [Validators.required],
    });
    const taskTemplateFile = fb.control<TuiFileLike | null>(null, {
      validators: [Validators.required],
    });

    return fb.group({
      customerInfo,
      executorInfo,
      contractInfo,
      actTemplateFile,
      taskTemplateFile,
    });
  }
}
