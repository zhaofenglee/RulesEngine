import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { RulesEngineComponent } from './components/rules-engine.component';
import { RulesEngineRoutingModule } from './rules-engine-routing.module';

@NgModule({
  declarations: [RulesEngineComponent],
  imports: [CoreModule, ThemeSharedModule, RulesEngineRoutingModule],
  exports: [RulesEngineComponent],
})
export class RulesEngineModule {
  static forChild(): ModuleWithProviders<RulesEngineModule> {
    return {
      ngModule: RulesEngineModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<RulesEngineModule> {
    return new LazyModuleFactory(RulesEngineModule.forChild());
  }
}
