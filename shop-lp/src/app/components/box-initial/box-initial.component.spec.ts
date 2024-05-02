import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoxInitialComponent } from './box-initial.component';

describe('BoxInitialComponent', () => {
  let component: BoxInitialComponent;
  let fixture: ComponentFixture<BoxInitialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoxInitialComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BoxInitialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
