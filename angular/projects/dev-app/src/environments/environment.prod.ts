import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'RulesEngine',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44356/',
    redirectUri: baseUrl,
    clientId: 'RulesEngine_App',
    responseType: 'code',
    scope: 'offline_access RulesEngine',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44356',
      rootNamespace: 'JS.Abp.RulesEngine',
    },
    RulesEngine: {
      url: 'https://localhost:44380',
      rootNamespace: 'JS.Abp.RulesEngine',
    },
  },
} as Environment;
