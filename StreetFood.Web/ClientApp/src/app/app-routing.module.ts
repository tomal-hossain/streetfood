import { FoodComponent } from './food/food.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
    {
      path: '',
      redirectTo: 'food',
      pathMatch: 'full'
    },
    {
      path: 'food',
      loadChildren: () => import('./food/food.module').then(m => m.FoodModule)
    },
    {
      path: 'authentication',
      loadChildren: () => import('./authentication/authentication.module').then(m => m.AutheticationModule)
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
