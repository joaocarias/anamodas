import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoxInitialDescriptionComponent } from './box-initial-description.component';

describe('BoxInitialDescriptionComponent', () => {
  let component: BoxInitialDescriptionComponent;
  let fixture: ComponentFixture<BoxInitialDescriptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoxInitialDescriptionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BoxInitialDescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
