import {View} from './view';
import {Vote} from './vote';
import {Anwer} from './anwer';

export class Question{
    id : string;
    userId : string;
    title : string;
    description: string;
    tags : string[];
    views : View[];
    votes : Vote[];
    anwers : Anwer[];
    updationTime: Date;
}










