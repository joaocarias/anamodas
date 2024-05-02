import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoxInitialCardsComponent } from './box-initial-cards.component';

describe('BoxInitialCardsComponent', () => {
  let component: BoxInitialCardsComponent;
  let fixture: ComponentFixture<BoxInitialCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoxInitialCardsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BoxInitialCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
