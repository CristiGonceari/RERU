import { HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { ResponseArray } from "./response.model";
import { UserModel } from "./user.model";

export interface UserProfileServiceModel {
    get: (params: HttpParams) => Observable<ResponseArray<UserModel>>;
}
