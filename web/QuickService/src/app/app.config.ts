import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { tokenInterceptor } from './core/interceptors/token-interceptors';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
     provideHttpClient(
          withFetch(),
          withInterceptors([tokenInterceptor])


     ),
     provideClientHydration()]
};
