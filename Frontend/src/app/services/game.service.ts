import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {BehaviorSubject, Observable, switchMap} from "rxjs";
import {GameNextStep, GameStory} from "../interfaces/gameCard";
import {AuthService} from "./auth.service";

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService
  ) {}

  public eventStartGame = new BehaviorSubject<void>(undefined);
  
  public getStory(): Observable<GameStory> {
    return this._authService.token$.pipe(
      switchMap((token) => this._httpClient.post<GameStory>(`api/Game/new-game/${token}`, {}))
    )
  }

  public getNextStepStory(answer: string): Observable<GameNextStep> {
    let roomId = localStorage.getItem("roomId");
    return this._httpClient.post<GameNextStep>(`api/Game/new-game/${roomId}`, {answer: answer});
  }

  public getGame(): Observable<GameStory> {
    let roomId = localStorage.getItem("roomId");
    return this._httpClient.get<GameStory>(`api/Game/${roomId}`);
  }

  public generateGameStory(): void {
    this.eventStartGame.next();
  }
}
