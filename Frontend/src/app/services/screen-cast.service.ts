import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IGameNextStep } from '../interfaces/gameCard';

@Injectable({
  providedIn: 'root'
})
export class ScreenCastService {

  constructor(private readonly _httpClient: HttpClient) { }

  public getNewData(): Observable<IGameNextStep[]> {
    return this._httpClient.get<IGameNextStep[]>(`api/ScreenCaset/get-data`);
  }
}
