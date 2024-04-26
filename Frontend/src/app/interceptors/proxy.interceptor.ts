import {HttpInterceptorFn} from "@angular/common/http";
import { environment } from "../../environments/environment.development";

export const proxyInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.url.startsWith('api/')) {
    req = req.clone({url: `${environment.apiRoot}${req.urlWithParams}`});
  }
  return next(req);
}
