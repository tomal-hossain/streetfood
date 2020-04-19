import { Router, ActivatedRoute } from '@angular/router';
import { FoodService } from './../../../shared/service/food.service';
import { IFood } from './../../../shared/interface/food';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ICountry } from 'src/shared/interface/country';
import { NzMessageService, UploadXHRArgs } from 'ng-zorro-antd';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-add-food',
    templateUrl: './add-food.component.html'
})
export class AddFoodComponent implements OnInit {

    foodForm: FormGroup;
    isLoadingFlag: boolean;
    countryList: ICountry[];
    imageBaseUrl: string;
    imageUrl: string;
    selectedFood: IFood;

    constructor(
        private fb: FormBuilder,
        private foodService: FoodService,
        private messageSerive: NzMessageService,
        private router: Router,
        private activatedRoute: ActivatedRoute) {
            this.imageBaseUrl = environment.apiUrl + '/images/';
        }

    ngOnInit() {
        this.foodForm = this.fb.group({
            name: [null, [ Validators.required, Validators.minLength(3), Validators.maxLength(30)] ],
            imageUrl: [null ],
            description: [null, [ Validators.required, Validators.minLength(300)] ],
            popularInList: [null, [ Validators.required ] ],
        });
        this.foodService.getCountries()
            .subscribe(response => {
                this.countryList = response;
            });
        this.activatedRoute.paramMap.subscribe(params => {
            const id = +params.get('id');
            if (id > 0) {
                this.foodService.getFood(id)
                    .subscribe(response => {
                        this.selectedFood = response;
                        this.setCurrentValue();
                    }, error => {
                        this.messageSerive.error('No food found!');
                        this.router.navigate(['food/my-food']);
                    });
            }
        });
    }

    setCurrentValue() {
        this.foodForm.patchValue(this.selectedFood);
        this.imageUrl = this.selectedFood.imageUrl;
    }

    compareFn(c1: ICountry, c2: ICountry): boolean {
        return c1 && c2 ? c1.id === c2.id : c1 === c2;
    }

    uploadImage = (item: UploadXHRArgs) => {
        const model: FormData = new FormData();
        model.append('file', item.file as any);
        return this.foodService.uploadImage(model)
            .subscribe(response => {
                this.imageUrl = response.url;
            });
    }

    makeFormDirty(form: FormGroup) {
        Object.keys(form.controls).forEach(key => {
            const control = form.controls[key];
            control.markAsDirty();
            control.updateValueAndValidity();
        });
    }

    submitForm(formValue: IFood) {
        this.makeFormDirty(this.foodForm);
        if (this.foodForm.valid) {
            this.isLoadingFlag = true;
            formValue.imageUrl = this.imageUrl;
            if (this.selectedFood) {
                formValue.id = this.selectedFood.id;
                this.foodService.editFood(formValue)
                    .subscribe(response => {
                        this.messageSerive.success('Successfully updated!');
                        this.router.navigate(['food/my-foods']);
                    }, error => {
                        this.messageSerive.error('Unable to update food. Please try again!');
                        this.isLoadingFlag = false;
                    });
            } else {
                this.foodService.addFood(formValue)
                    .subscribe(response => {
                        this.messageSerive.success('Successfully added!');
                        this.router.navigate(['food/my-foods']);
                    }, error => {
                        this.messageSerive.error('Unable to add food. Please try again!');
                        this.isLoadingFlag = false;
                    });
            }
        }
    }

}
