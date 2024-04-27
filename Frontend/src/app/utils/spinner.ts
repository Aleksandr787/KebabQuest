import {Observable, of, switchMap} from "rxjs";
import {computed, Signal, signal, WritableSignal} from "@angular/core";

export class Spinner {
  private readonly _value: WritableSignal<number> = signal(0);

  public readonly active: Signal<boolean> = computed(() => this._value() !== 0);
  public readonly passive: Signal<boolean> = computed(() => this._value() === 0);

  public enter(): void {
    this._value.update(value => value + 1)
  }

  public leave(): void {
    this._value.update(value => value - 1);
  }
}

export function switchMapSpinner<TIn, TOut>(project: (value: TIn) => Observable<TOut>, spinner: Spinner | undefined) {
  return switchMap<TIn, Observable<TOut>>((value) => {
    spinner?.enter();
    let active = true;

    return new Observable<TOut>((subscriber) => {
      const subscription = project(value).subscribe({
        next: (val) => {
          subscriber.next(val);
          if (active) {
            spinner?.leave();
            active = false;
          }
        },
        error: () => {
          if (active) {
            spinner?.leave();
            active = false;
          }
        }
      });

      return () => {
        subscription.unsubscribe();
        if (active) {
          spinner?.leave();
          active = false;
        }
      };
    });
  });
}

export function withSpinner<TOut>(source: Observable<TOut>, spinner: Spinner | undefined) {
  return of(0).pipe(switchMapSpinner<unknown, TOut>(() => source, spinner));
}

