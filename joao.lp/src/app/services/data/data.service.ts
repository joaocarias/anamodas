import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Apiconfig } from "../../utils/appconfig";

@Injectable({
  providedIn: 'root',
  })
  export class DataService {

    public urlApi = Apiconfig.getApiUrl();

    constructor(private http: HttpClient) {
  
    }

    // public composeHeaders() {
    //     const token = security.getToken();
    //     const headers = new HttpHeaders().set('Authorization', `bearer ${token}`);
    //     return headers;
    //   }

    postMensagem(data: any){
        return this.http.post(`${this.urlApi}/Mensagem`, data );
    }
  }