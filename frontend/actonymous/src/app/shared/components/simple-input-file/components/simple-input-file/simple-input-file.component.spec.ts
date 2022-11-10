import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleInputFileComponent } from './simple-input-file.component';

describe('SimpleInputFileComponent', () => {
  let component: SimpleInputFileComponent;
  let fixture: ComponentFixture<SimpleInputFileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleInputFileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SimpleInputFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
