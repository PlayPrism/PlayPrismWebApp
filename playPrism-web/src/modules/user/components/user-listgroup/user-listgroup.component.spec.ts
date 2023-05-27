import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserListgroupComponent } from './user-listgroup.component';

describe('UserListgroupComponent', () => {
  let component: UserListgroupComponent;
  let fixture: ComponentFixture<UserListgroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserListgroupComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserListgroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
