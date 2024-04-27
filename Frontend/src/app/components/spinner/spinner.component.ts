import {Component} from "@angular/core";
import {AsyncPipe, NgIf} from "@angular/common";
import {MatProgressSpinner} from "@angular/material/progress-spinner";
import {interval, map} from "rxjs";

@Component({
  selector: 'app-spinner',
  standalone: true,
  imports: [
    AsyncPipe,
    MatProgressSpinner,
    NgIf
  ],
  template: `
    <div
      class="spinner">
      <mat-spinner></mat-spinner>
      <p style="color: var(--primary-text); font-size: 24px; font-weight: 500;">Подождите{{ dots$ | async }}</p>
    </div>
  `,
  styles: `
    .spinner {
      position: fixed;
      width: 100%;
      height: 100%;
      top: 0;
      left: 0;
      z-index: 9999999;
      justify-content: center;
      align-items: center;
      display: flex;
      flex: 1;
      flex-direction: column;
      background: rgba(25, 28, 28, .6);
      gap: 16px;
    }
  `
})
export class SpinnerComponent {
  private readonly _values = ['.', '..', '...'];

  protected readonly dots$ = interval(500).pipe(
    map((index) => this._values[index % this._values.length])
  );
}
