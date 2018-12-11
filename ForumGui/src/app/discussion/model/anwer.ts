import {Vote} from './vote';
import {Comment} from './comment';
import {UserView} from './userView';

export class Anwer{
    id:string;
    anwerBy :string;
    userView : UserView;
    content: string;
    creationTime: Date;
    updationTime: Date;
    votes : Vote[];
    comment: Comment[];
}

