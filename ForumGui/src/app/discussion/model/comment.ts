import {UserView} from './userView';

export class Comment{
    commentBy : string;
    userView : UserView;
    content :string;
    creationTime: Date;
    updationTime : Date;
}
