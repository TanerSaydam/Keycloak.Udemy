import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { KeycloakAuthGuard, KeycloakService } from 'keycloak-angular'

@Injectable({
  providedIn: 'root'
})
export class AuthGuard extends KeycloakAuthGuard{

  constructor(
    protected override readonly router: Router,
    protected readonly keycloak: KeycloakService
  ){
    super(router, keycloak);
  }

  public async isAccessAllowed(
    route: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot) {
    if(!this.authenticated){
      await this.keycloak.login({
        redirectUri: window.location.origin + state.url,
      });
    }

    const requiredRoles = route.data['roles'];

    if(!Array.isArray(requiredRoles) || requiredRoles.length === 0){
      return true;
    }

    return requiredRoles.every((role)=> this.roles.includes(role));
  }  
  
}
