import { Component, OnDestroy, OnInit } from '@angular/core';
import { IGameNextStep } from '../../interfaces/gameCard';
import { ScreenCastService } from '../../services/screen-cast.service';
import {Subscription, interval, switchMap, of, concat} from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-screen-cast',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './screen-cast.component.html',
  styleUrl: './screen-cast.component.scss'
})
export class ScreenCastComponent implements OnInit, OnDestroy {

  public screenCastData: IGameNextStep[] = [];
  private pollingSubscription!: Subscription;

  constructor(private readonly _screenCastService: ScreenCastService) {}

  public ngOnInit(): void {
    this.startPollingData();
  }

  public ngOnDestroy(): void {
    this.pollingSubscription.unsubscribe();
  }

  private startPollingData(): void {
    this.pollingSubscription = concat(of(0), interval(7000))
    .pipe(
      switchMap(() => this._screenCastService.getNewData())
    ).subscribe(data => this.screenCastData = data);
  }
}
