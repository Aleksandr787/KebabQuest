import { Routes } from '@angular/router';
import { StartPageComponent } from './components/start-page/start-page.component';
import { GamePageComponent } from './components/game-page/game-page.component';

export const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: 'start'
    },
    {
      path: 'start',
      component: StartPageComponent,
    },
    {
      path: 'game',
      component: GamePageComponent,
    },
  ];