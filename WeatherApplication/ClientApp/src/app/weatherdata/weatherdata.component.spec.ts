import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeatherdataComponent } from './weatherdata.component';

describe('WeatherdataComponent', () => {
  let component: WeatherdataComponent;
  let fixture: ComponentFixture<WeatherdataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeatherdataComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeatherdataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
