import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoxInitialCarouselComponent } from './box-initial-carousel.component';

describe('BoxInitialCarouselComponent', () => {
  let component: BoxInitialCarouselComponent;
  let fixture: ComponentFixture<BoxInitialCarouselComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoxInitialCarouselComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BoxInitialCarouselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
