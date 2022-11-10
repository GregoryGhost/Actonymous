import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExecutorInfoComponent } from './executor-info.component';

describe('ExecutorInfoComponent', () => {
  let component: ExecutorInfoComponent;
  let fixture: ComponentFixture<ExecutorInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExecutorInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExecutorInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
