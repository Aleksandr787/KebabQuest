import {HttpInterceptorFn} from "@angular/common/http";
import {environment} from "../../environments/environment";

export const proxyInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.url.startsWith('api/')) {
    req = req.clone({
      url: `${environment.apiUrl}/${req.urlWithParams}`, withCredentials: true
    });
  }
  return next(req);
}
