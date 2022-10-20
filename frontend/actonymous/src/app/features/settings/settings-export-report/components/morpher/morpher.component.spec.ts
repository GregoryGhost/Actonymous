import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MorpherComponent } from './morpher.component';

describe('MorpherComponent', () => {
  let component: MorpherComponent;
  let fixture: ComponentFixture<MorpherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MorpherComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MorpherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
