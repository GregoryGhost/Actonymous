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
  minLengthValidator,
  requiredInputMsg,
  ConnectionPingStatuses,
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
        pattern: 'Invalid the access token',
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

  public readonly accessTokenMask = {
    guid: true,
    mask: MorpherComponent.getAccessTokenMask(),
  };

  constructor(private readonly fb: FormBuilder) {
    super(MorpherComponent.initForm(fb));
  }

  ngOnInit(): void {}

  public pingMorpher(event: MouseEvent): void {
    this.pingConnection = ConnectionPingStatuses.Pending;
  }

  private static initForm(fb: FormBuilder): MorpherForm {
    const accessTokenMaskPattern = Validators.pattern(
      /[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}/
    );

    return fb.group({
      accessToken: fb.control('', {
        nonNullable: true,
        validators: [
          Validators.required,
          Validators.maxLength(36),
          Validators.minLength(36),
          accessTokenMaskPattern,
        ],
      }),
    });
  }

  private static getAccessTokenMask(): (string | RegExp)[] {
    const maskSymbols8: RegExp[] = Array(8).fill(/[a-z0-9]/);
    const maskSymbols4: RegExp[] = Array(4).fill(/[a-z0-9]/);
    const maskSymbols12: RegExp[] = Array(12).fill(/[a-z0-9]/);
    const mask = [
      ...maskSymbols8,
      `-`,
      ...maskSymbols4,
      `-`,
      ...maskSymbols4,
      `-`,
      ...maskSymbols4,
      `-`,
      ...maskSymbols12,
    ];

    return mask;
  }
}
