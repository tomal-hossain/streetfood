import { IFood } from './../../../shared/interface/food';
import { FoodService } from './../../../shared/service/food.service';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
    selector: 'app-my-foods',
    templateUrl: './my-foods.component.html',
})
export class MyFoodsComponent implements OnInit {

    foodList: IFood[];
    imageBaseUrl: string;

    constructor(private foodService: FoodService, private messageService: NzMessageService) {
        this.imageBaseUrl = environment.apiUrl + '/images/';
    }

    deleteConfirm(id: number) {
        this.foodService.deleteFood(id)
            .subscribe(response => {
                this.messageService.success('Successfully deleted!');
                this.foodList = this.foodList.filter(x => x.id !== id);
            }, error => {
                this.messageService.error('Unable to delete this. Please try again!');
            });
    }

    ngOnInit() {
        this.foodService.getMyFoods()
            .subscribe(response => {
                this.foodList = response;
            });
    }
}
