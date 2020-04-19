import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ICountry } from '../interface/country';
import { HttpClient } from '@angular/common/http';
import { IFood } from '../interface/food';
import { IUrl } from '../interface/url';

@Injectable({
    providedIn: 'root',
})

export class FoodService {

    apiUrl: string;

    constructor(private http: HttpClient) {
        this.apiUrl = environment.apiUrl + '/api/food';
    }

    getCountries(): Observable<ICountry[]> {
        return this.http.get<ICountry[]>(this.apiUrl + '/country');
    }

    getAllFoods(): Observable<IFood[]> {
        return this.http.get<IFood[]>(this.apiUrl);
    }

    getMyFoods(): Observable<IFood[]> {
        return this.http.get<IFood[]>(this.apiUrl + '/ownfood');
    }

    getFood(id: number): Observable<IFood> {
        return this.http.get<IFood>(this.apiUrl + '/' + id);
    }

    addFood(model: IFood) {
        return this.http.post(this.apiUrl, model);
    }

    editFood(model: IFood) {
        return this.http.put(this.apiUrl, model);
    }

    deleteFood(id: number) {
        return this.http.delete(this.apiUrl + '/' + id);
    }

    uploadImage(model: FormData): Observable<IUrl> {
        return this.http.post<IUrl>(this.apiUrl + '/image', model);
    }
}
