import { ActivatedRoute, Router } from '@angular/router';
import { FoodService } from './../../../shared/service/food.service';
import { Component, OnInit } from '@angular/core';
import { IFood } from 'src/shared/interface/food';
import { NzMessageService } from 'ng-zorro-antd';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-view-food',
    templateUrl: './view-food.component.html'
})
export class ViewFoodComponent implements OnInit {

    selectedFood: IFood;
    imageBaseUrl: string;

    constructor(
        private foodService: FoodService,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private messageService: NzMessageService) {
          this.imageBaseUrl = environment.apiUrl + '/images/';
        }

    ngOnInit() {
        this.activatedRoute.paramMap.subscribe(params => {
            const foodId = +params.get('id');
            if (foodId > 0) {
                this.foodService.getFood(foodId)
                    .subscribe(response => {
                        this.selectedFood = response;
                    }, error => {
                        this.messageService.error('No food found!');
                        this.router.navigate(['food/all-foods']);
                    });
            }
        });
    }

}
