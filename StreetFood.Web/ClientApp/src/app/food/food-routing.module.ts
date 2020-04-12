import { AuthGuard } from '../../shared/guard/auth.guard';
import { AddFoodComponent } from './add-food/add-food.component';
import { AllFoodsComponent } from './all-foods/all-food.component';
import { MyFoodsComponent } from './my-foods/my-foods.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';


const routes: Routes = [
    {
        path: '',
        redirectTo: 'all-foods',
        pathMatch: 'full'
    },
    {
        path: 'all-foods',
        component: AllFoodsComponent
    },
    {
        path: 'my-foods',
        component: MyFoodsComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'add-food',
        component: AddFoodComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'edit-food/:id',
        component: AddFoodComponent,
        canActivate: [AuthGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class FoodRoutingModule { }
