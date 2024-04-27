import {Injectable} from "@angular/core";
import {Observable, of, switchMap, tap} from "rxjs";
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
      this.reg().pipe(tap((token) => this.setToken(token))))
  );

  public reg(): Observable<string> {
    return this._httpClient.post<string>('api/User/RegisterUser', {});
  }

  public setToken(token: string): void {
    localStorage.setItem(this._key, token);
  }
}
