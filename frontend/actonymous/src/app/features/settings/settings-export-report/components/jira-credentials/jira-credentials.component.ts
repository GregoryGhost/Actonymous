import {
  ChangeDetectionStrategy,
  Component,
  forwardRef,
  Input,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import {
  ControlValueAccessor,
  FormBuilder,
  FormControl,
  FormGroup,
  NG_VALUE_ACCESSOR,
  Validators,
} from '@angular/forms';
import { ConnectionPingStatuses, Form } from 'src/app/shared/';
import { JiraCredentials } from '../../models';

type JiraCredentialsForm = FormGroup<Form<JiraCredentials>>;

@Component({
  selector: 'jira-credentials',
  templateUrl: './jira-credentials.component.html',
  styleUrls: ['./jira-credentials.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [{ 
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => JiraCredentialsComponent),
    multi: true
   }]
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
      serverAddress: ''
    };
    this.form = this.fb.group({
      login: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      password: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      serverAddress: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
    });
  }

  ngOnInit(): void {}

  writeValue(value: JiraCredentials): void {
    this.form.setValue(value);
  }

  registerOnChange(fn: (value: Partial<JiraCredentials>) => void): void {
    this.form.valueChanges.subscribe(fn);
  }

  registerOnTouched(fn: any): void {}

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.form.disable();
    } else {
      this.form.enable();
    }
  }

  public pingJiraServer(event: MouseEvent): void {
    this.pingJiraConnectionStatus = ConnectionPingStatuses.Pending;
  }
}
