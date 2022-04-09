import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginAuth: AuthService) { }

  ngOnInit(): void {
  }
  loginForm= new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    pwd: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]),
  });

  isUserValid: boolean=false;

loginSubmitted(){
  this.loginAuth
  .loginUser(
    [this.loginForm.value.email,
    this.loginForm.value.pwd]
  ).subscribe((res)=>
  {
    if(res=='Invalid'){
      this.isUserValid=false;
      alert("Login Failed");
    }else{
      this.isUserValid=true;
      alert("Login Successfull");
    }
  }
  );
}

  get Email(): FormControl{
    return this.loginForm.get('email') as FormControl;
  }
  get Pwd(): FormControl{
    return this.loginForm.get('pwd') as FormControl;
  }
}
