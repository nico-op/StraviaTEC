import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VistaDeportistaComponent } from './vista-deportista.component';

describe('VistaDeportistaComponent', () => {
  let component: VistaDeportistaComponent;
  let fixture: ComponentFixture<VistaDeportistaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VistaDeportistaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VistaDeportistaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
