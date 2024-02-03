import { environment } from "../../environments/environment";

export class Apiconfig {
   
    public static getApiUrl() {
       return environment.protocolUrl + "://" + environment.baseUrl + ":" + environment.port + "/" + environment.baseApiUrl ;
    }
}