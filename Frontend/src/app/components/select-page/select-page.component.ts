import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatRippleModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { Router, RouterOutlet } from '@angular/router';
import { IGameCard } from '../../interfaces/gameCard';
import { GameService } from '../../services/game.service';

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
    MatProgressSpinnerModule
  ],
  templateUrl: './select-page.component.html',
  styleUrl: './select-page.component.scss'
})
export class SelectPageComponent implements OnInit {
  
  public cards: IGameCard[] = [];

  constructor(
    private _gameService: GameService,
    private _router: Router
  ) {}

  public ngOnInit(): void {
    // this.loadGameCards();   
  }

  public loadGameCards(): void {
    // создать сервис User и там стучатся до контролера по типу getUserGameCards и доставать те карточки, которые есть у юзера 
    // ( ЧТобы он мог продолжить игру)
    // id проверять есть ли он в куках или локал сторадже и тогда доставать карточки иначе просто предлагать рандомную тему
  }

  public selectGame(): void {
    // this._gameService.eventStartGame.emit();
    this._router.navigate(["game"]);
  }
}
