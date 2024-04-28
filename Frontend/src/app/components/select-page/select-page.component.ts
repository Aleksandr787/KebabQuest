import {CommonModule} from '@angular/common';
import {Component} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatRippleModule} from '@angular/material/core';
import {MatDividerModule} from '@angular/material/divider';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatRadioModule} from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';
import {Router, RouterOutlet} from '@angular/router';
import {IGameCard} from '../../interfaces/gameCard';
import {GameSampleService} from "../../services/game-sample.service";
import {Observable, shareReplay} from "rxjs";
import {AppRoutes, GlobalQueryParams} from "../../app.routes";
import {SpinnerComponent} from "../spinner/spinner.component";
import {Spinner, withSpinner} from "../../utils/spinner";

@Component({
  selector: 'app-select-page',
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
    MatRippleModule,
    MatProgressSpinnerModule,
    SpinnerComponent
  ],
  templateUrl: './select-page.component.html',
  styleUrl: './select-page.component.scss'
})
export class SelectPageComponent {
  constructor(
    private readonly _gameSampleService: GameSampleService,
    private readonly _router: Router
  ) {
  }

  protected readonly spinner = new Spinner();

  protected readonly cards$: Observable<IGameCard[]> = withSpinner(this._gameSampleService.getSamples(), this.spinner).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  protected selectGame(card: IGameCard | null = null): void {
    this._router.navigate([AppRoutes.GAME], {
      queryParams: {[GlobalQueryParams.TEMPLATE_ID]: card?.id}
    }).then();
  }

  protected goBack(): void {
    this._router.navigate([AppRoutes.START]).then();
  }
}
