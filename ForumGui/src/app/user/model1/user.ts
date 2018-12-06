import {Activity} from '../model1/activity';
import {Education} from '../model1/education';
import {Experience} from '../model1/experience';
import {Objective} from '../model1/objective';
import {Skill} from '../model1/skill';

export class User{
    id : string;
    name : string;
    gender : boolean; 
    birthday : Date;
    phoneNumber : string;
    address : string;
    position : string;
    username : string;
    emailAddress : string;
    avatar : string;
    updationTime : Date;
    objectives : Objective [];
    Educations : Education[] ;
    Skills : Skill[];
    Experiences : Experience [];
    Activities : Activity[];
}


