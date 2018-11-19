import {User} from '../user/user';
export interface Question {
  id: string,
  title: string,
  content: string,
  user: User,
  dateCreated: Date ,
  votes: number,
  answers: number,
  views: number,
}