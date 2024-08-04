import { Component, signal } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { FlexiToastService } from 'flexi-toast';
import { FlexiGridModule } from 'flexi-grid';
import { RoleModel } from '../../models/role.model';

@Component({
  selector: 'app-roles',
  standalone: true,
  imports: [FlexiGridModule],
  templateUrl: './roles.component.html',
  styleUrl: './roles.component.css'
})
export class RolesComponent {
  roles = signal<RoleModel[]>([]);

  constructor(
    private http: HttpService,
    private toast: FlexiToastService
  ){
    this.getAll();
  }

  getAll(){
    this.http.get<RoleModel[]>("Roles/GetAll",(res)=> {
      this.roles.set(res);
    });
  }

  deleteById(name: string){
    this.toast.showSwal("Delete Role?", "You want to delete this role?",()=> {
      this.http.delete<string>(`Roles/DeleteByName?name=${name}`, res=> {
        this.toast.showToast("Info",res, "info");
        this.getAll();
      });
    })
  }
}
