import {makeEnvironmentProviders} from '@angular/core';
import { RULES_ENGINE_ROUTE_PROVIDERS } from './providers/route.provider';

export function provideRulesEngineConfig() {
  return makeEnvironmentProviders([RULES_ENGINE_ROUTE_PROVIDERS])
}
