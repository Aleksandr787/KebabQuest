import { Component, NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { FormsModule } from '@angular/forms';
import { GameService } from '../../services/game.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { GameStory } from '../../interfaces/gameCard';

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
  ],
  templateUrl: './game-page.component.html',
  styleUrl: './game-page.component.scss'
})
export class GamePageComponent implements OnInit {
  constructor(
    private readonly _gameService: GameService,
    private readonly _router: Router
  ) {
  }

  public isActive: boolean = false;
  private _isLoading: boolean = false;

  protected readonly govno: string[] = [
    'Lorem Lorem Lorem', 'Lorem Lorem Lorem', 'Lorem Lorem Lorem',
  ]

  public story: GameStory | undefined;

  public ngOnInit(): void {
    this._gameService.eventStartGame.subscribe(() => {
      this.generateGameStory();
    });
  }

  get isLoading(): boolean {
    return this._isLoading;
  }

  public generateGameStory() {
    console.log('generateGameStory');
    this._isLoading = true;

    this._gameService.getStory().subscribe((story: GameStory) => {
      this.story = story;
      this._isLoading = false;
    });
  }


  public toStartPage() {
    this._router.navigate(["start"]);
  }


  activeElement: number | null = null;

  public toggleActive(elementId: number): void {
    this.activeElement = elementId;
  }

  protected readonly Object = Object;
}