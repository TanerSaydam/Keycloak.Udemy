import { Component, signal } from '@angular/core';
import { FlexiGridModule } from 'flexi-grid';
import { UserModel } from '../../models/user.model';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [FlexiGridModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent {
  users = signal<UserModel[]>([]);

  constructor(
    private http: HttpService
  ){
    this.getAll();
  }

  getAll(){
    this.http.get<UserModel[]>("Users/GetAll",(res)=> {
      this.users.set(res);
    });
  }
}
