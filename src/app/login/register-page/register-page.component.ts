import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router'

import { RegisterUserService } from '../_services/registerUser.service'
import { UserService } from '../../landingPage/_services/userService';
import { RegisterUser } from '../_models/registerUser.model'

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {

  loading: boolean = false;
  registerForm: FormGroup;
  singleSiteMode: boolean;
  constructor(private fb: FormBuilder,
    private userService: RegisterUserService,
    private router: Router) {
    this.createForm();
  }

  createForm() {
    this.registerForm = this.fb.group({
      siteId: ['', [Validators.required, Validators.maxLength(50)]],
      firstName: ['', [Validators.required, Validators.maxLength(50)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.maxLength(100), Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    },
    {
      validator: PasswordValidation.MatchPassword // your validation method
    }
    );
  }

  ngOnInit() {
    this.userService.CheckIfSingleSite()
      .subscribe(result => {
        console.log(JSON.stringify(result));
        this.singleSiteMode = result;
        if (this.singleSiteMode) {
          this.registerForm.patchValue({ siteId: 'dummyValue' });
        }
      })
  }

  onSubmit() {
    const formModel = this.registerForm.value;
    let newUser = new RegisterUser(formModel.siteId,
      formModel.firstName,
      formModel.lastName,
      formModel.email,
      formModel.password);
    this.userService.RegisterNewUser(newUser)
      .subscribe(result => {
        if (result === true) {
          this.router.navigate(['/login']);
        }
        else {
          this.loading = false;
        }
      },
      error => console.log(error))
  }

}

export class PasswordValidation {

  static MatchPassword(AC: AbstractControl) {
     let password = AC.get('password').value; // to get value in input tag
     let confirmPassword = AC.get('confirmPassword').value; // to get value in input tag
      if(password != confirmPassword) {
          console.log('false');
          AC.get('confirmPassword').setErrors( {MatchPassword: true} )
      } else {
          console.log('true');
          return null
      }
  }
}
