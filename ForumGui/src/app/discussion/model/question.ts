import {View} from './view';
import {Vote} from './vote';
import {Anwer} from './anwer';
import {Tag} from './tag'

export class Question{
    id : string;
    userId : string;
    title : string;
    description: string;
    tags : Tag[];
    views : View[];
    votes : Vote[];
    anwers : Anwer[];
    updationTime: Date;
}










