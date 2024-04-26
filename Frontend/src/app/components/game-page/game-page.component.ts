import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatRadioModule} from '@angular/material/radio';
import {FormsModule} from '@angular/forms';
import { GameStory, GameService } from '../../services/game.service';
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
    FormsModule
  ],
  templateUrl: './game-page.component.html',
  styleUrl: './game-page.component.scss'
})
export class GamePageComponent {
  public answers: string[] = ['Lorem ipsum dolor sit amet, consectetur adipisicing elit.', 'Voluptates corporis enim sed, eum debitis eius id earum modi deserunt at eveniet quas inventore tempore perspiciatis aperiam blanditiis', 'Summer'];

  public story: GameStory | undefined;
  
  constructor(
    private _gameService: GameService,
    private _router: Router

  ) {}

  public generateGameStory() {
    this._gameService.getStory().subscribe((story: GameStory) => {
      this.story = story;
    });
  }

  public toStartPage(){
    this._router.navigate(["start"]);
  }
  
}