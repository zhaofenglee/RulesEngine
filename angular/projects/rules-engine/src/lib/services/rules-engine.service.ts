import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class RulesEngineService {
  apiName = 'RulesEngine';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/RulesEngine/sample' },
      { apiName: this.apiName }
    );
  }
}
