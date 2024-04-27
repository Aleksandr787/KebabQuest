import {Injectable} from "@angular/core";
import {map, Observable, of, switchMap} from "rxjs";
import {fromLocalStorage} from "../utils/local-storage";
import {HttpClient} from "@angular/common/http";

@Injectable({providedIn: 'root'})
export class AuthService {
  public constructor(private readonly _httpClient: HttpClient) {
  }

  private readonly _key = 'JWT_KEY';

  public readonly tokenNullable$: Observable<string | undefined> = fromLocalStorage(this._key);

  public readonly token$: Observable<string> = this.tokenNullable$.pipe(
    switchMap((token) => token ?
      of(token) :
      this.reg().pipe(map((token) => {
        this.setToken(token.id);
        return token.id
      })))
  );

  public reg(): Observable<{ id: string }> {
    return this._httpClient.post<{ id: string }>('api/User/RegisterUser', {});
  }

  public setToken(token: string): void {
    localStorage.setItem(this._key, token);
  }
}
