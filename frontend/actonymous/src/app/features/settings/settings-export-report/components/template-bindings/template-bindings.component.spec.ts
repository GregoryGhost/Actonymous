import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TemplateBindingsComponent } from './template-bindings.component';

describe('TemplateBindingsComponent', () => {
  let component: TemplateBindingsComponent;
  let fixture: ComponentFixture<TemplateBindingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TemplateBindingsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TemplateBindingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
