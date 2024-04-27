import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IGameCard} from "../interfaces/gameCard";

@Injectable({providedIn: 'root'})
export class GameSampleService {
  constructor(private readonly _httpClient: HttpClient) {
  }

  public getSamples(): Observable<IGameCard[]> {
    return this._httpClient.post<IGameCard[]>('/api/GameSample/get-samples', {});
  }
}
