import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RulesEngineComponent } from './components/rules-engine.component';
import { RulesEngineService } from '@j-s.Abp/rules-engine';
import { of } from 'rxjs';

describe('RulesEngineComponent', () => {
  let component: RulesEngineComponent;
  let fixture: ComponentFixture<RulesEngineComponent>;
  const mockRulesEngineService = jasmine.createSpyObj('RulesEngineService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [RulesEngineComponent],
      providers: [
        {
          provide: RulesEngineService,
          useValue: mockRulesEngineService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RulesEngineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
