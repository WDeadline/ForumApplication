import {View} from './view';
import {Vote} from './vote';
import {Answer} from './answer';
import {Tag} from './tag';
import {UserView} from './userView';
import {Report} from './report';

export class Question{
    id : string;
    userId : string;
    userView : UserView;
    title : string;
    description: string;
    tags : Tag[] = [];
    views : View[] = [];
    votes : Vote[] = [];
    reports : Report[] = [];
    answers : Answer[] = [];
    updationTime: Date;
}










