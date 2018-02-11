import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import {RegisterUserService}from '../_services/registerUser.service'
import { UserService } from '../../landingPage/_services/userService';

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
  private userService: RegisterUserService) { 
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
    });
  }

  ngOnInit() {
    this.userService.CheckIfSingleSite()
    .subscribe(result => {
      console.log(JSON.stringify(result))
      this.singleSiteMode = result})
  }

  onSubmit() {
    const formModel = this.registerForm.value;
    
  }

}
