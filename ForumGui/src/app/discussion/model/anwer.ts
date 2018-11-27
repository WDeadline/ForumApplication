import {Vote} from './vote';
import {Comment} from './comment'
export class Anwer{
    anwerBy :string;
    content: string;
    creationTime: Date;
    updationTime: Date;
    votes : Vote[];
    comment: Comment[];
}

