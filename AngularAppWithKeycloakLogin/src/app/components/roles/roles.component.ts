import { Component, ElementRef, signal, ViewChild } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { FlexiToastService } from 'flexi-toast';
import { FlexiGridModule } from 'flexi-grid';
import { RoleModel } from '../../models/role.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-roles',
  standalone: true,
  imports: [FlexiGridModule, FormsModule],
  templateUrl: './roles.component.html',
  styleUrl: './roles.component.css'
})
export class RolesComponent {
  roles = signal<RoleModel[]>([]);
  name = signal<string>("");
  description = signal<string>("");

  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

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

  save(){
    this.http.post<string>("Roles/Create", {name: this.name(), description: this.description()},(res)=> {
      this.toast.showToast("Info",res, "info");
      this.name.set("");
      this.getAll();

      this.addModalCloseBtn!.nativeElement.click();
    });
  }
}
