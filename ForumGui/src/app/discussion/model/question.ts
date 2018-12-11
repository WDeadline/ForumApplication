import {View} from './view';
import {Vote} from './vote';
import {Anwer} from './anwer';
import {Tag} from './tag';
import {UserView} from './userView';
import {Report} from './report';

export class Question{
    id : string;
    userId : string;
    userView : UserView;
    title : string;
    description: string;
    tags : Tag[];
    views : View[];
    votes : Vote[];
    reports : Report[];
    anwers : Anwer[];
    updationTime: Date;
}










