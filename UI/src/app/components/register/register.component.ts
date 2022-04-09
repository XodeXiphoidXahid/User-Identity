import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  repeatPass: string='none';
  constructor() { }

  ngOnInit(): void {
  }

  registerForm= new FormGroup({
    email: new FormControl("", [Validators.required, Validators.email]),
    pwd: new FormControl("", 
    [Validators.required,
      Validators.minLength(6),
      Validators.maxLength(15),
    ]),
    cPwd: new FormControl("")
  });

  registerSubmitted(){
    if(this.Pwd.value==this.Cpwd.value){
      console.log("Submitted");
      this.repeatPass='none'
    }else{
      this.repeatPass='inline'
    }
  }

  get Email(): FormControl{
    return this.registerForm.get("email") as FormControl;
  }

  get Pwd(): FormControl{
    return this.registerForm.get("pwd") as FormControl;
  }
  get Cpwd(): FormControl{
    return this.registerForm.get("cPwd") as FormControl;
  }
}

