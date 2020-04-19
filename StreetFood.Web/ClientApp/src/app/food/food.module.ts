import { AuthGuard } from './../../shared/guard/auth.guard';
import { NgModule } from '@angular/core';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzFormModule, NzInputModule, NzButtonModule, NzPopoverModule, NzListModule, NzSelectModule, NzTableModule  } from 'ng-zorro-antd';
import { NzGridModule, NzSpinModule, NzLayoutModule, NzMenuModule, NzAvatarModule, NzMessageModule, NzUploadModule  } from 'ng-zorro-antd';
import { NzIconModule, NzTagModule, NzPopconfirmModule } from 'ng-zorro-antd';
import { CommonModule } from '@angular/common';
import { FoodRoutingModule } from './food-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AllFoodsComponent } from './all-foods/all-foods.component';
import { MyFoodsComponent } from './my-foods/my-foods.component';
import { AddFoodComponent } from './add-food/add-food.component';
import { FoodComponent } from './food.component';
import { ViewFoodComponent } from './view-food/view-food.component';

@NgModule({
    declarations: [
        AllFoodsComponent,
        MyFoodsComponent,
        AddFoodComponent,
        FoodComponent,
        ViewFoodComponent
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
        NzSpinModule,
        NzLayoutModule,
        NzMenuModule,
        NzAvatarModule,
        NzPopoverModule,
        NzListModule,
        NzSelectModule,
        NzMessageModule,
        NzUploadModule,
        NzTableModule,
        NzIconModule,
        NzTagModule,
        NzPopconfirmModule
    ],
    providers: [ AuthGuard ]
})
export class FoodModule { }
