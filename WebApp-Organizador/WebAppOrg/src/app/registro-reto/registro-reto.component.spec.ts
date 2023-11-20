import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroRetoComponent } from './registro-reto.component';

describe('RegistroRetoComponent', () => {
  let component: RegistroRetoComponent;
  let fixture: ComponentFixture<RegistroRetoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegistroRetoComponent]
    });
    fixture = TestBed.createComponent(RegistroRetoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
