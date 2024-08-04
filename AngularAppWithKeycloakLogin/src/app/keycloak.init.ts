import { KeycloakService } from "keycloak-angular";

export function initializeKeycloak(keycloak: KeycloakService){
    return () => 
        keycloak.init({
            config: {
                url: 'http://localhost:8080',
                realm: 'myrealm',
                clientId: 'angular-client'
            },
            enableBearerInterceptor: true,
            bearerPrefix: "Bearer",
            bearerExcludedUrls: ['/assets'],
            initOptions: {
                onLoad: 'check-sso',
                silentCheckSsoRedirectUri:
                    window.location.origin + '/assets/slient-check-sso.html',
            }
        });
}