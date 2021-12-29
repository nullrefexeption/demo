import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Flight} from "../shared/models/flight.model";
import {TableParams} from "../shared/models/table-params.model";
import { TableResults } from '../shared/models/table-results.model';
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";
import { ApiResponse } from '../shared/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class FlightsService {
  baseUrl = environment.apiUrl;

  constructor(private readonly http: HttpClient) { }

  public getAll(tableParams: TableParams): Observable<TableResults<Flight>> {
    return this.http.post<TableResults<Flight>>(this.baseUrl + 'flights/list', tableParams)
      .pipe(
        map((res: TableResults<Flight>) => res)
      );
  }

  public add(flight: any): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.baseUrl + 'flights', flight)
      .pipe(
        map((res: ApiResponse) => res)
      );
  }

  public update(flight: any): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl + 'flights', flight)
      .pipe(
        map((res: ApiResponse) => res)
      );
  }

  public changeDelay(flight: Flight): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.baseUrl + 'flights/changeDelay', flight)
      .pipe(
        map((res: ApiResponse) => res)
      );
  }

  public delete(flight: Flight): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + 'flights/' + `${flight.id}`)
      .pipe(
        map((res: ApiResponse) => res)
      );
  }
}
