import { RouterOutletComponent } from '@abp/ng.core';
import { Routes } from '@angular/router';
import { RulesEngineComponent } from './components/rules-engine.component';

export const rulesEngineRoutes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: RouterOutletComponent,
    children: [
      {
        path: '',
        component: RulesEngineComponent,
      },
    ],
  },
];
