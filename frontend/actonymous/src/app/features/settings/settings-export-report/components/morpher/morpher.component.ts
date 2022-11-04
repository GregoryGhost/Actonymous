import {
  ChangeDetectionStrategy,
  Component,
  forwardRef,
  OnInit
} from '@angular/core';
import {
  FormBuilder,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  Validators
} from '@angular/forms';
import { UntilDestroy } from '@ngneat/until-destroy';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import {
  BaseFormGroup,
  FormData,
  maxLengthValidator,
  minLengthValidator,
  requiredInputMsg,
  ConnectionPingStatuses
} from 'src/app/shared';
import { MorpherInfo } from '../../models';

type MorpherForm = FormData<MorpherInfo>;

@UntilDestroy()
@Component({
  selector: 'morpher',
  templateUrl: './morpher.component.html',
  styleUrls: ['./morpher.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MorpherComponent),
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
      useExisting: forwardRef(() => MorpherComponent),
      multi: true,
    },
  ],
})
export class MorpherComponent
  extends BaseFormGroup<MorpherInfo>
  implements OnInit
{
  public pingConnection = ConnectionPingStatuses.Idle;

  constructor(private readonly fb: FormBuilder) {
    super(MorpherComponent.initForm(fb));
  }

  ngOnInit(): void {}

  public pingMorpher(event: MouseEvent): void {
    this.pingConnection = ConnectionPingStatuses.Pending;
  }

  private static initForm(fb: FormBuilder): MorpherForm {
    return fb.group({
      accessToken: fb.control('', {
        nonNullable: true,
        validators: [
          Validators.required,
          Validators.maxLength(36),
          Validators.minLength(36),
        ],
      }),
    });
  }
}
