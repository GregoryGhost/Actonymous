import { Component, OnInit } from '@angular/core';
import { TuiFileLike } from '@taiga-ui/kit';
import { empty, Observable, of } from 'rxjs';
import {
  JiraCredentials,
  MorpherInfo,
  SettingsExportReport,
  TemplateBindingsInfo,
} from '../../models';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss'],
})
export class PageComponent implements OnInit {
  public readonly data$: Observable<SettingsExportReport>;

  constructor() {
    const emptyFile = null as TuiFileLike | null;
    const templateBindings: TemplateBindingsInfo = {
      customerInfo: {
        companyName: '',
        headerFullname: '',
        headerPosition: '',
      },
      executorInfo: {
        companyName: '',
        headerFullname: '',
        headerPosition: '',
        ratePerHour: 0,
      },
      contractInfo: {
        contractNumber: '',
        approvalDate: new Date(),
        contractFile: emptyFile,
      },
      actTemplateFile: emptyFile,
      taskTemplateFile: emptyFile,
    };
    const jiraCredentials: JiraCredentials = {
      login: '',
      password: '',
      serverAddress: '',
    };
    const morpher: MorpherInfo = { accessToken: '' };
    this.data$ = of({
      jiraCredentials,
      morpher,
      templateBindings,
    });
  }

  ngOnInit(): void {}

  public submit(data: SettingsExportReport): void {
    const jsonData = JSON.stringify(data);
    console.log(jsonData);
  }
}
