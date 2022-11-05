import {
  ChangeDetectionStrategy,
  Component,
  forwardRef,
  Input,
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
  ConnectionPingStatuses,
  FormData,
  maxLengthValidator,
  minLengthValidator,
  requiredInputMsg,
} from 'src/app/shared/';
import { JiraCredentials } from '../../models';

type JiraCredentialsForm = FormData<JiraCredentials>;

@UntilDestroy()
@Component({
  selector: 'jira-credentials',
  templateUrl: './jira-credentials.component.html',
  styleUrls: ['./jira-credentials.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => JiraCredentialsComponent),
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
      useExisting: forwardRef(() => JiraCredentialsComponent),
      multi: true,
    },
  ],
})
export class JiraCredentialsComponent
  extends BaseFormGroup<JiraCredentials>
  implements OnInit
{
  public pingJiraConnectionStatus = ConnectionPingStatuses.Idle;

  constructor(private readonly fb: FormBuilder) {
    super(JiraCredentialsComponent.initForm(fb));
  }

  ngOnInit(): void {}

  public pingJiraServer(event: MouseEvent): void {
    this.pingJiraConnectionStatus = ConnectionPingStatuses.Pending;
  }

  private static initForm(fb: FormBuilder): JiraCredentialsForm {
    return fb.group({
      login: fb.control('', {
        nonNullable: true,
        validators: [Validators.required, Validators.maxLength(20)],
      }),
      password: fb.control('', {
        nonNullable: true,
        validators: [Validators.required, Validators.maxLength(50)],
      }),
      serverAddress: fb.control('', {
        nonNullable: true,
        validators: [Validators.required, Validators.maxLength(250)],
      }),
    });
  }
}
