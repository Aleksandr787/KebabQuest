import {BehaviorSubject, map, Observable, of, switchMap} from "rxjs";

export class Spinner {
  public readonly value$ = new BehaviorSubject<number>(0);
  public readonly active$ = this.value$.pipe(
    map((v) => {
      return v !== 0;
    })
  );
  public readonly passive$ = this.value$.pipe(
    map((v) => {
      return v === 0;
    })
  );


  public enter() {
    this.value$.next(this.value$.value + 1);
  }

  public leave() {
    this.value$.next(this.value$.value - 1);
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

