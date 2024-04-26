import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class GameService {

    constructor(
        private _httpClient: HttpClient,
    ) { }

    public getStory(): Observable<GameStory> {
        console.log("Get gameStory");
        return this._httpClient.get<GameStory>('');
    }

}

export interface GameStory {
    text: string;
    image: string;
}