export class ResultModel<T>{    
    data?: T;
    errorMessages?: string[];
    isSuccessful: boolean = true;
}