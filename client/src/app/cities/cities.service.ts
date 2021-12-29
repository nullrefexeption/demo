import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {map} from "rxjs/operators";
import {City} from "../shared/models/city.model";

@Injectable({
  providedIn: 'root'
})
export class CitiesService {

  baseUrl = environment.apiUrl;

  constructor(private readonly http: HttpClient) { }

  public getAll(): Observable<Array<City>> {
    return this.http.get<Array<City>>(this.baseUrl + 'cities')
      .pipe(
        map((res: Array<City>) => res)
      );
  }
}
