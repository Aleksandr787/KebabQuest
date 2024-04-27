import {Observable} from "rxjs";

export function fromLocalStorage(key: string): Observable<string | undefined> {
  return new Observable<string | undefined>((subscriber) => {
    subscriber.next(localStorage.getItem(key) || undefined);

    const handler = (e: StorageEvent) => {
      if (e.key === key && e.oldValue === e.newValue) {
        subscriber.next(e.newValue || undefined)
      }
    }

    window.addEventListener('storage', handler);

    return () => window.removeEventListener('storage', handler)
  })
}
