import {UserView} from './userView';
import {Tag} from '../discussion/model/tag';
export class Work {
    id: string;
    companyId : string;
    userView : UserView = new UserView();
    title : string;
    position : string;
    description : string;
    address : string;
    salary : string;
    tags : Tag[];
    updationTime: Date;
  }