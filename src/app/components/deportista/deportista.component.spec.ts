import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeportistaComponent } from './deportista.component';

describe('DeportistaComponent', () => {
  let component: DeportistaComponent;
  let fixture: ComponentFixture<DeportistaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeportistaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DeportistaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
