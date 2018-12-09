import {Activity} from '../model1/activity';
import {Education} from '../model1/education';
import {Experience} from '../model1/experience';
import {Objective} from '../model1/objective';
import {Skill} from '../model1/skill';
import {Role} from '../model1/role';

export class User{
    id : string;
    name : string;
    active : boolean;
    roles : Role[] = [];
    gender : boolean; 
    birthday : Date;
    phoneNumber : string;
    address : string;
    position : string;
    password :string;
    username : string;
    emailAddress : string;
    avatar : string;
    updationTime : Date;
    objectives : Objective [] = [];
    educations : Education[]  = [];
    skills : Skill[] = [];
    experiences : Experience [] = [];
    activities : Activity[] = [];
}


