import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoxInitialImagemComponent } from './box-initial-imagem.component';

describe('BoxInitialImagemComponent', () => {
  let component: BoxInitialImagemComponent;
  let fixture: ComponentFixture<BoxInitialImagemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoxInitialImagemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BoxInitialImagemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
