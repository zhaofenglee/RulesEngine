import { TestBed } from '@angular/core/testing';
import { RulesEngineService } from './services/rules-engine.service';
import { RestService } from '@abp/ng.core';

describe('RulesEngineService', () => {
  let service: RulesEngineService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(RulesEngineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
