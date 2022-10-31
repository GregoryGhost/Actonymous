import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConnectionPingerComponent } from './connection-pinger.component';

describe('ConnectionPingerComponent', () => {
  let component: ConnectionPingerComponent;
  let fixture: ComponentFixture<ConnectionPingerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConnectionPingerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConnectionPingerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
