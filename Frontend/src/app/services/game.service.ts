import { HttpClient } from "@angular/common/http";
import { EventEmitter, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GameStory } from "../interfaces/gameCard";

@Injectable({
    providedIn: 'root'
})
export class GameService {
    
    public eventStartGame: EventEmitter<any> = new EventEmitter<any>();

    constructor(
        private _httpClient: HttpClient,
    ) { }

    public getStory(): Observable<GameStory> {
        console.log("Get gameStory");
        return this._httpClient.get<GameStory>('');
    }
}