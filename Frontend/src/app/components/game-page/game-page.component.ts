import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ActivatedRoute, Router, RouterOutlet} from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatRadioModule} from '@angular/material/radio';
import {FormsModule} from '@angular/forms';
import {GameService} from '../../services/game.service';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {IGameNextStep, IGameStory} from '../../interfaces/gameCard';
import {AppRoutes, GlobalQueryParams} from "../../app.routes";
import {combineLatest, concat, first, map, Observable, of, shareReplay, Subject, tap} from "rxjs";
import {Spinner, switchMapSpinner} from "../../utils/spinner";
import {SpinnerComponent} from "../spinner/spinner.component";

@Component({
  selector: 'app-game-page',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatRadioModule,
    FormsModule,
    MatProgressSpinnerModule,
    SpinnerComponent,
  ],
  templateUrl: 'game-page.component.html',
  styleUrl: 'game-page.component.scss'
})
export class GamePageComponent {
  constructor(
    private readonly _gameService: GameService,
    private readonly _router: Router,
    private readonly _route: ActivatedRoute
  ) {
  }

  protected readonly spinner = new Spinner();

  private readonly _templateId$: Observable<string | null> = this._route.queryParamMap.pipe(
    map((params) => params.get(GlobalQueryParams.TEMPLATE_ID))
  );
  private readonly _gameId$: Observable<string | null> = this._route.queryParamMap.pipe(
    map((params) => params.get(GlobalQueryParams.GAME_ID))
  )

  private readonly _stepEvent$: Subject<string> = new Subject<string>();

  protected readonly _step$: Observable<IGameNextStep> = this._stepEvent$.pipe(
    switchMapSpinner((answer) => this._gameService.getNextStepStory(answer), this.spinner)
  );

  protected readonly story$: Observable<IGameStory> = combineLatest([
    this._templateId$,
    this._gameId$
  ]).pipe(
    first(),
    switchMapSpinner(([templateId, gameId]) => {
      return gameId ? this._gameService.getGame(gameId) :
        templateId ? this._gameService.getStory(templateId) :
          this._gameService.getRandomStory();
    }, this.spinner),
    tap((story) => this._appendGameId(story.id)),
    switchMapSpinner((story) => concat(of(null), this._step$).pipe(
      map((step) => this._bindStep(story, step))
    ), this.spinner),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  private _appendGameId(gameId: string) {
    return this._router.navigate([], {
      queryParams: {
        [GlobalQueryParams.GAME_ID]: gameId
      },
      queryParamsHandling: 'merge'
    });
  }

  private _bindStep(game: IGameStory, step: IGameNextStep | null = null): IGameStory {
    if (step === null) return game;

    game.image = step.image;
    game.options = step.options;
    game.question = step.question;

    return game;
  }

  protected activeElement: number | null = null;

  protected toStartPage() {
    this._router.navigate([AppRoutes.START]).then();
  }

  protected toggleActive(elementId: number): void {
    this.activeElement = elementId;
  }

  protected get isDisabled(): boolean {
    return this.activeElement === null;
  }

  protected nextStep(answer: string): void {
    this._stepEvent$.next(answer);
  }

  protected readonly Object = Object;
}
