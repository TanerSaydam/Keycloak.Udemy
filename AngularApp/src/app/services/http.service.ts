import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { ResultModel } from '../models/result.model';
import { FlexiToastService } from 'flexi-toast';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  api = signal<string>("https://localhost:7042/api");

  constructor(
    private http :HttpClient,
    private toast: FlexiToastService
  ) { }

  get<T>(endpoint: string, callback: (res:T)=> void){
    this.http.get<ResultModel<T>>(`${this.api()}/endpoint`).subscribe({
      next: (res)=> {
        callback(res.data!);
      },
      error: ((err: HttpErrorResponse) => {
          if(err.status === 401 || err.status === 403){
            this.toast.showToast("Error","You are not authorized", "error");            
          }else{
            if(err.error.errorMessages){
              const e = err.error.errorMessages;
              e.forEach((el:string) => {
                this.toast.showToast("Error",el, "error");    
              });
            }else{
              this.toast.showToast("Error","Something went wrong", "error");
            }
          }
      }),
    })  
  }

  post<T>(endpoint: string, body: any, callback: (res:T)=> void){
    this.http.post<ResultModel<T>>(`${this.api()}/endpoint`, body).subscribe({
      next: (res)=> {
        callback(res.data!);
      },
      error: ((err: HttpErrorResponse) => {
          if(err.status === 401 || err.status === 403){
            this.toast.showToast("Error","You are not authorized", "error");            
          }else{
            if(err.error.errorMessages){
              const e = err.error.errorMessages;
              e.forEach((el:string) => {
                this.toast.showToast("Error",el, "error");    
              });
            }else{
              this.toast.showToast("Error","Something went wrong", "error");
            }
          }
      }),
    })
  }

  put<T>(endpoint: string, body: any, callback: (res:T)=> void){
    this.http.put<ResultModel<T>>(`${this.api()}/endpoint`, body).subscribe({
      next: (res)=> {
        callback(res.data!);
      },
      error: ((err: HttpErrorResponse) => {
          if(err.status === 401 || err.status === 403){
            this.toast.showToast("Error","You are not authorized", "error");            
          }else{
            if(err.error.errorMessages){
              const e = err.error.errorMessages;
              e.forEach((el:string) => {
                this.toast.showToast("Error",el, "error");    
              });
            }else{
              this.toast.showToast("Error","Something went wrong", "error");
            }
          }
      }),
    })
  }

  delete<T>(endpoint: string, callback: (res:T)=> void){
    this.http.delete<ResultModel<T>>(`${this.api()}/endpoint`).subscribe({
      next: (res)=> {
        callback(res.data!);
      },
      error: ((err: HttpErrorResponse) => {
          if(err.status === 401 || err.status === 403){
            this.toast.showToast("Error","You are not authorized", "error");            
          }else{
            if(err.error.errorMessages){
              const e = err.error.errorMessages;
              e.forEach((el:string) => {
                this.toast.showToast("Error",el, "error");    
              });
            }else{
              this.toast.showToast("Error","Something went wrong", "error");
            }
          }
      }),
    })
  }

  deleteWithBody<T>(endpoint: string, body:any, callback: (res:T)=> void){
    this.http.delete<ResultModel<T>>(`${this.api()}/endpoint`, body).subscribe({
      next: (res:any)=> {
        callback(res.data!);
      },
      error: ((err: HttpErrorResponse) => {
          if(err.status === 401 || err.status === 403){
            this.toast.showToast("Error","You are not authorized", "error");            
          }else{
            if(err.error.errorMessages){
              const e = err.error.errorMessages;
              e.forEach((el:string) => {
                this.toast.showToast("Error",el, "error");    
              });
            }else{
              this.toast.showToast("Error","Something went wrong", "error");
            }
          }
      }),
    })
  }
}
