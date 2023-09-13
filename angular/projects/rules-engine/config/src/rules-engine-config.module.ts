import { ModuleWithProviders, NgModule } from '@angular/core';
import { RULES_ENGINE_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class RulesEngineConfigModule {
  static forRoot(): ModuleWithProviders<RulesEngineConfigModule> {
    return {
      ngModule: RulesEngineConfigModule,
      providers: [RULES_ENGINE_ROUTE_PROVIDERS],
    };
  }
}
