import {City} from "./city.model";

export interface Flight {
  id?: number;
  departureCity?: City;
  arrivalCity?: City;
  departureTime?: Date;
  arrivalTime?: Date;
  delay?: number;
}
