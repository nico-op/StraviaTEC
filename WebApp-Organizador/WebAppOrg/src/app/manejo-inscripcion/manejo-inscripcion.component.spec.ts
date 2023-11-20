import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManejoInscripcionComponent } from './manejo-inscripcion.component';

describe('ManejoInscripcionComponent', () => {
  let component: ManejoInscripcionComponent;
  let fixture: ComponentFixture<ManejoInscripcionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManejoInscripcionComponent]
    });
    fixture = TestBed.createComponent(ManejoInscripcionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
