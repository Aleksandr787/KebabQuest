import {Component} from "@angular/core";
import {AsyncPipe, NgIf} from "@angular/common";
import {MatProgressSpinner} from "@angular/material/progress-spinner";

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
      style="display: flex; flex-direction: column; gap: 24px; height: 100%; width: 100%; align-items: center; justify-content: center; margin-top: 150px;">
      <mat-spinner></mat-spinner>
      <p class="dots" style="color: var(--primary-text); font-size: 24px; font-weight: 500;">Подождите...</p>
    </div>
  `,
  styles: `
    .dots {
      display: inline-block;
      animation: dot-animation 1.5s infinite;
      animation-delay: 0s, 0.5s, 1s;
    }
  `
})
export class SpinnerComponent {

}
