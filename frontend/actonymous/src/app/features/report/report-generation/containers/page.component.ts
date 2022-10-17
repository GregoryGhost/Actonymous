import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { TuiDay, TuiDayRange } from '@taiga-ui/cdk';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss'],
})
export class PageComponent implements OnInit {
  public readonly minDate: TuiDay;
  public readonly maxDate: TuiDay;

  public readonly pageForm: FormGroup;

  constructor(private readonly fb: FormBuilder) {
    this.minDate = new TuiDay(2000, 1, 1);
    this.maxDate = new TuiDay(9999, 1, 1);
    this.pageForm = this.fb.group({
      reportPeriod: [null, [Validators.required]],
    });
  }

  ngOnInit(): void {}

  public submit(): void {
    //TODO: implement
  }
}
