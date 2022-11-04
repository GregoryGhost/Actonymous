import {
  ChangeDetectionStrategy,
  Component,
  forwardRef,
  Input,
  OnInit,
} from '@angular/core';
import {
  ControlValueAccessor,
  FormBuilder,
  FormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { ConnectionPingStatuses, Form, maxLengthValidator, minLengthValidator, requiredInputMsg } from 'src/app/shared/';
import { JiraCredentials } from '../../models';

type JiraCredentialsForm = FormGroup<Form<JiraCredentials>>;

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
export class JiraCredentialsComponent implements OnInit, ControlValueAccessor {
  @Input()
  public jiraCreds: JiraCredentials;

  public pingJiraConnectionStatus = ConnectionPingStatuses.Idle;

  public form: JiraCredentialsForm;

  constructor(private readonly fb: FormBuilder) {
    this.jiraCreds = {
      login: '',
      password: '',
      serverAddress: '',
    };
    this.form = this.fb.group({
      login: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required, Validators.maxLength(20)],
      }),
      password: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required, Validators.maxLength(50)],
      }),
      serverAddress: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required, Validators.maxLength(250)],
      }),
    });
  }

  ngOnInit(): void {}

  writeValue(value: JiraCredentials): void {
    this.form.setValue(value);
  }

  registerOnChange(fn: (value: Partial<JiraCredentials>) => void): void {
    this.form.valueChanges.pipe(untilDestroyed(this)).subscribe(fn);
  }

  registerOnTouched(fn: any): void {}

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.form.disable();
    } else {
      this.form.enable();
    }
  }

  validate(): ValidationErrors | null {
    return this.form.invalid
      ? { invalid: true }
      : null;
  }

  public pingJiraServer(event: MouseEvent): void {
    this.pingJiraConnectionStatus = ConnectionPingStatuses.Pending;
  }
}
