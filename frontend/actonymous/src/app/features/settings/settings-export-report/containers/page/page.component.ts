import { Component, OnInit } from '@angular/core';
import { empty, Observable, of } from 'rxjs';
import { SettingsExportReport } from '../../models';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss'],
})
export class PageComponent implements OnInit {
  public readonly data$: Observable<SettingsExportReport>;

  constructor() {
    this.data$ = of({
      jiraCredentials: { login: '', password: '', serverAddress: '' },
      morpher: { accessToken: '' }
    });
  }

  ngOnInit(): void {}

  public submit(data: SettingsExportReport): void {
    //TODO: implement
  }
}
