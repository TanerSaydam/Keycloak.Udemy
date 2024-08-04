import { Routes } from '@angular/router';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { HomeComponent } from './components/home/home.component';
import { UsersComponent } from './components/users/users.component';
import { RolesComponent } from './components/roles/roles.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [    
    {
        path: "",
        component: LayoutsComponent,
        canActivate: [AuthGuard],
        children: [
            {
                path: "",
                component: HomeComponent
            },
            {
                path: "users",
                component: UsersComponent
            },
            {
                path: "roles",
                component: RolesComponent
            }
        ]
    }
];
