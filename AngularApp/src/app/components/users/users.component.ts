import { Component, signal } from '@angular/core';
import { FlexiGridModule } from 'flexi-grid';
import { UserModel } from '../../models/user.model';
import { HttpService } from '../../services/http.service';
import { FlexiToastService } from 'flexi-toast';

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
    private http: HttpService,
    private toast: FlexiToastService
  ){
    this.getAll();
  }

  getAll(){
    this.http.get<UserModel[]>("Users/GetAll",(res)=> {
      this.users.set(res);
    });
  }

  deleteById(id: string){
    this.toast.showSwal("Delete User?", "You want to delete this user?",()=> {
      this.http.delete<string>(`Users/DeleteById?id=${id}`, res=> {
        this.toast.showToast("Info",res, "info");
        this.getAll();
      });
    })
  }
}
