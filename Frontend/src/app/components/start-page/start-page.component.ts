import {CommonModule} from '@angular/common';
import {Component} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatDividerModule} from '@angular/material/divider';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatRadioModule} from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';
import {Router, RouterOutlet} from '@angular/router';
import {AppRoutes, GlobalQueryParams} from "../../app.routes";
import {Observable} from "rxjs";
import {IGameStory} from "../../interfaces/gameCard";
import {GameService} from "../../services/game.service";
import {MatRipple} from "@angular/material/core";
import {SpinnerComponent} from "../spinner/spinner.component";
import {Spinner, withSpinner} from "../../utils/spinner";

@Component({
  selector: 'app-start-page',
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
    MatRipple,
    SpinnerComponent
  ],
  templateUrl: './start-page.component.html',
  styleUrl: './start-page.component.scss'
})
export class StartPageComponent {

  constructor(
    private readonly _router: Router,
    private readonly _gameService: GameService
  ) {
  }

  protected readonly spinner = new Spinner();

  protected readonly gameStories$: Observable<IGameStory[]> = withSpinner(this._gameService.getGames(), this.spinner);

  protected startGame(): void {
    this._router.navigate([AppRoutes.CREATE]).then();
  }

  protected continueGame(id: string): void {
    this._router.navigate([AppRoutes.GAME], {
      queryParams: {[GlobalQueryParams.GAME_ID]: id}
    }).then();
  }
}
