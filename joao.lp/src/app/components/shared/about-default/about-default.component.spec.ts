import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutDefaultComponent } from './about-default.component';

describe('AboutDefaultComponent', () => {
  let component: AboutDefaultComponent;
  let fixture: ComponentFixture<AboutDefaultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AboutDefaultComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AboutDefaultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
