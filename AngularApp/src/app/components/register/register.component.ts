import { Component, signal } from '@angular/core';
import { RegisterModel } from '../../models/register.model';
import { HttpService } from '../../services/http.service';
import { Router, RouterLink } from '@angular/router';
import { FlexiToastService } from 'flexi-toast';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  model = signal<RegisterModel>(new RegisterModel());

  constructor(
    private http: HttpService,
    private router: Router,
    private toast: FlexiToastService
  ){}

  login(){
    this.http.post<string>("Auth/Register", this.model(), ()=> {      
      this.router.navigateByUrl("/login");
      this.toast.showToast("Success","Register is successful. You can login now");
    });
  }
}
