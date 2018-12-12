import {UserView} from './userView';

export class Comment{
    id: string;
    commentBy : string;
    userView : UserView;
    content :string;
    creationTime: Date;
    updationTime : Date;
}
