import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable, switchMap} from "rxjs";
import {IGameNextStep, IGameStory} from "../interfaces/gameCard";
import {AuthService} from "./auth.service";

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService
  ) {
  }

  public getGames(): Observable<IGameStory[]> {
    return this._authService.token$.pipe(
      switchMap((token) => this._httpClient.get<IGameStory[]>(`api/Game/get-all-games/${token}`))
    );
  }

  public getNextStepStory(gameId: string, answer: string): Observable<IGameNextStep> {
    return this._httpClient.post<IGameNextStep>(`api/Game/do-step/${gameId}`, {answer: answer});
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

  public getGame(gameId: string): Observable<IGameStory> {
    return this._httpClient.get<IGameStory>(`api/Game/${gameId}`);
  }
}
