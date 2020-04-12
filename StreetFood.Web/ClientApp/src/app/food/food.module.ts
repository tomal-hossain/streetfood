import { AuthGuard } from './../../shared/guard/auth.guard';
import { NgModule } from '@angular/core';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzFormModule, NzInputModule, NzButtonModule, NzGridModule, NzSpinModule } from 'ng-zorro-antd';
import { CommonModule } from '@angular/common';
import { FoodRoutingModule } from './food-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AllFoodsComponent } from './all-foods/all-food.component';
import { MyFoodsComponent } from './my-foods/my-foods.component';
import { AddFoodComponent } from './add-food/add-food.component';

@NgModule({
    declarations: [
        AllFoodsComponent,
        MyFoodsComponent,
        AddFoodComponent
    ],
    imports: [
        CommonModule,
        FoodRoutingModule,
        NzCardModule,
        NzFormModule,
        NzInputModule,
        ReactiveFormsModule,
        NzButtonModule,
        NzGridModule,
        NzSpinModule
    ],
    providers: [ AuthGuard ],
})
export class FoodModule { }
