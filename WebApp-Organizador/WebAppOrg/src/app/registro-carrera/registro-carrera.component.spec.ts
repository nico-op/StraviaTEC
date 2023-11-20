import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroCarreraComponent } from './registro-carrera.component';

describe('RegistroCarreraComponent', () => {
  let component: RegistroCarreraComponent;
  let fixture: ComponentFixture<RegistroCarreraComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegistroCarreraComponent]
    });
    fixture = TestBed.createComponent(RegistroCarreraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
