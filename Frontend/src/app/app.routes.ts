import {Routes} from '@angular/router';
import {StartPageComponent} from './components/start-page/start-page.component';
import {GamePageComponent} from './components/game-page/game-page.component';
import {SelectPageComponent} from './components/select-page/select-page.component';

export enum AppRoutes {
  START = 'start',
  CREATE = 'create',
  GAME = 'game'
}

export enum GlobalQueryParams {
  TEMPLATE_ID = 'templateId',
  GAME_ID = 'gameId'
}

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: AppRoutes.START
  },
  {
    path: AppRoutes.START,
    component: StartPageComponent,
  },
  {
    path: AppRoutes.CREATE,
    component: SelectPageComponent,
  },
  {
    path: AppRoutes.GAME,
    component: GamePageComponent,
  },
];
