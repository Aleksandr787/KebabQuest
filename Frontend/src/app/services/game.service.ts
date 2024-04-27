import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable, switchMap} from "rxjs";
import {GameStory} from "../interfaces/gameCard";
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

  public getStory(): Observable<GameStory> {
    return this._authService.token$.pipe(
      switchMap((token) => this._httpClient.post<GameStory>(`api/Game/new-game/${token}`, {}))
    )
  }
}
