import {Vote} from './vote';
import {Comment} from './comment';
import {UserView} from './userView';

export class Answer{
    id:string;
    answerBy :string;
    userView : UserView;
    content: string;
    creationTime: Date;
    updationTime: Date;
    votes : Vote[] = [];
    comments: Comment[] = [];
}

