import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  repeatPass: string='none';
  displayMessage: string='';
  isAccountCreated: boolean=false;
  constructor(private authService: AuthService) { }

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

      this.authService.registerUser(
        [this.registerForm.value.email,
          this.registerForm.value.pwd,
          this.registerForm.value.cPwd
        ]
      ).subscribe(res=>{
        if(res=='Success'){
          this.displayMessage='Account Created Successfully!';
          this.isAccountCreated=true;
        }else{
          this.displayMessage='Something went wrong';
          this.isAccountCreated=false;
        }
      });
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
//https://localhost:44386/

