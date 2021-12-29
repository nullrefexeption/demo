import {Component, OnInit, ViewChild} from '@angular/core';
import {Flight} from "../shared/models/flight.model";
import {TableParams} from "../shared/models/table-params.model";
import {FlightsService} from "./flights.service";
import {NotificationService} from "../core/services/notification.service";
import {Table} from "primeng/table";
import {City} from "../shared/models/city.model";
import {CitiesService} from "../cities/cities.service";
import {ApiResponseStatus} from "../shared/models/api-response.model";

@Component({
  selector: 'app-flights',
  templateUrl: './flights.component.html',
  styleUrls: ['./flights.component.css']
})
export class FlightsComponent implements OnInit {
  @ViewChild('flightsTable') table!: Table;

  flights: Flight[] = [];
  loadingTable = false;
  ordersCount: number = 0;
  lastParams: TableParams = new TableParams();
  departureDateFilter: any;
  arrivalDateFilter: any;
  displayDialog = false;
  cities: City[] = [];
  selectedDepartureCity: any;
  selectedArrivalCity: any;
  departureDate: any;
  arrivalDate: any;
  flight: Flight = {};
  isEdit = false;
  delay: number = 0;
  displayChangeDelayDialog = false;

  constructor(private flightService: FlightsService,
              private notificationService: NotificationService,
              private citiesService: CitiesService) { }

  ngOnInit(): void {
    this.loadCities();
  }

  loadCities() {
    this.citiesService.getAll().subscribe(
      result => {
        this.cities = result;
      },
      error => {
        this.notificationService.Error('Что-то пошло не так при загрузке списка городов');
      }
    )
  }

  public loadFlights(params?: TableParams) {
    let tableParams = new TableParams(params);
    if (params) {
      this.loadingTable = true;
      this.lastParams = tableParams;
    } else {
      tableParams = this.lastParams;
    }

    this.flightService.getAll(tableParams).subscribe(
      result => {
        this.loadingTable = false;
        this.flights = result.data;
        this.ordersCount = result.count;
      },
      () => {
        this.loadingTable = false;
        this.notificationService.Error('Что-то пошло не так');
      }
    );
  }

  public onArrivalDateSelect($event: any) {
    this.table.filter(this.formatDate($event), 'aDate', 'equals');
  }

  public onDepartureDateSelect($event: any) {
    this.table.filter(this.formatDate($event), 'dDate', 'equals');
  }

  private formatDate(date: any) {
    let month = date.getMonth() + 1;
    let day = date.getDate();
    if (month < 10) {
      month = '0' + month;
    }
    if (day < 10) {
      day = '0' + day;
    }
    return date.getFullYear() + '-' + month + '-' + day;
  }

  showAddFlightDialog() {
    this.isEdit = false;
    this.flight = {};
    this.displayDialog = true;
  }

  addFlight() {
    const flight =
      {
        arrivalCityId: this.selectedArrivalCity.id,
        departureCityId: this.selectedDepartureCity.id,
        arrivalTime: this.arrivalDate,
        departureTime: this.departureDate
      }

    this.flightService.add(flight).subscribe(result => {
      if (result.status === ApiResponseStatus.Success){
        this.notificationService.Success("Flight Added");
        this.loadFlights();
        this.displayDialog = false;
      }
    })
  }

  deleteFlight(flight: any) {
    this.notificationService.Confirm("Delete Flight","Are you sure?", () => {
      this.flightService.delete(flight).subscribe(result => {
        if (result.status === ApiResponseStatus.Success){
          this.notificationService.Success("Flight Deleted");
          this.loadFlights();
        } else {
          this.notificationService.Error("Flight was not Deleted");
        }
      }, error => {
        console.log(error);
      })
    })
  }

  editFlight(flight: Flight) {
    this.isEdit = true;
    this.flight = JSON.parse(JSON.stringify(flight));
    this.flight.departureTime = new Date(flight.departureTime!);
    this.flight.arrivalTime = new Date(flight.arrivalTime!);
    this.displayDialog = true;
  }

  saveFlight() {
    this.flightService.update(this.flight).subscribe(result => {
      if (result.status === ApiResponseStatus.Success){
        this.notificationService.Success("Flight Updated");
        this.loadFlights();
        this.displayDialog = false;
      }
    })
  }

  showChangeDelayDialog(flight: any) {
    this.flight = {...flight};
    this.displayChangeDelayDialog = true;
  }

  changeDelay() {
    this.flightService.changeDelay(this.flight).subscribe(result => {
      if (result.status === ApiResponseStatus.Success){
        this.notificationService.Success("Flight Delay Updated");
        this.loadFlights();
        this.displayChangeDelayDialog = false;
      }
    })
  }
}
