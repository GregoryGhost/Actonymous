import {
  ControlValueAccessor,
  FormControl,
  FormGroup,
  ValidationErrors,
} from '@angular/forms';
import { untilDestroyed } from '@ngneat/until-destroy';

export { BaseFormGroup, FormData, MainForm, BaseMainFormGroup };

type Form<T> = {
  [P in keyof T]: T[P] extends Date
    ? FormControl<T[P]>
    : T[P] extends object
    ? FormGroup<Form<T[P]>>
    : FormControl<T[P]>;
};

type RootForm<T> = {
  [P in keyof T]: FormControl<T[P]>;
};

type FormData<TData> = FormGroup<Form<TData>>;

type MainForm<TData> = FormGroup<RootForm<TData>>;

class BaseForm<TData, TFormType> implements ControlValueAccessor {
  public constructor(
    protected readonly form: FormGroup<
      TFormType extends string ? Form<TData> : RootForm<TData>
    >
  ) {}

  writeValue(value: TData): void {
    this.form.setValue(value as any);
  }

  registerOnChange(fn: (value: Partial<TData>) => void): void {
    this.form.valueChanges.pipe(untilDestroyed(this)).subscribe(fn as any);
  }

  registerOnTouched(fn: unknown): void {}

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.form.disable();
    } else {
      this.form.enable();
    }
  }

  validate(): ValidationErrors | null {
    return this.form.invalid ? { invalid: true } : null;
  }
}

type FormType = '';

type RootFormType = 0;

class BaseMainFormGroup<TData> extends BaseForm<TData, RootFormType> {}

class BaseFormGroup<TData> extends BaseForm<TData, FormType> {}
