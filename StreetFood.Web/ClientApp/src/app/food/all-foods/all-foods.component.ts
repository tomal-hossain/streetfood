import { IFood } from '../../../shared/interface/food';
import { FoodService } from '../../../shared/service/food.service';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-all-foods',
    templateUrl: './all-foods.component.html'
})
export class AllFoodsComponent implements OnInit {

    foodList: IFood[];
    imageBaseUrl: string;

    constructor(private foodService: FoodService) {
        this.imageBaseUrl = environment.apiUrl + '/images/';
    }

    ngOnInit() {
        this.foodService.getAllFoods()
            .subscribe(response => {
                this.foodList = response;
            });
    }

}
