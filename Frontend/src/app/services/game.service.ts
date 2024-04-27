import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {BehaviorSubject, Observable, switchMap} from "rxjs";
import {IGameNextStep, IGameStory} from "../interfaces/gameCard";
import {AuthService} from "./auth.service";

export enum GameStateEnum {
  START = 'START',
  IN_PROGRESS = 'IN_PROGRESS'
}

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService
  ) {
  }

  public readonly templateId$: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  public readonly gameState: BehaviorSubject<GameStateEnum> = new BehaviorSubject<GameStateEnum>(GameStateEnum.START);

  public getNextStepStory(answer: string): Observable<IGameNextStep> {
    let roomId = localStorage.getItem("roomId");
    return this._httpClient.post<IGameNextStep>(`api/Game/do-step/${roomId}`, {answer: answer});
  }

  public getStory(templateId: string): Observable<IGameStory> {
    return this._authService.token$.pipe(
      switchMap((token) => this._httpClient.get<IGameStory>(`api/Game/game-from-sample/${templateId}/${token}`))
    )
  }

  public getRandomStory(): Observable<IGameStory> {
    return this._authService.token$.pipe(
      switchMap((token) => this._httpClient.post<IGameStory>(`api/Game/new-game/${token}`, {}))
    )
  }

  public getGame(roomId: string): Observable<IGameStory> {
    return this._httpClient.get<IGameStory>(`api/Game/${roomId}`);
  }
}
