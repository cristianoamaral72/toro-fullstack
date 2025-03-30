/*!

 =========================================================
 * Light Bootstrap Dashboard Angular - v1.9.0
 =========================================================

 * Product Page: https://www.toroinvestimentos.com.br/product/light-bootstrap-dashboard-angular2
 * Copyright 2020Toro Investimento(https://www.toroinvestimentos.com.br)
 * Licensed under MIT

 =========================================================

 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 */
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule);
