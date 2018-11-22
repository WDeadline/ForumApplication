
export interface Question {
  id: string,
  userId : string,
  title: string,
  description: string,
  tags: string[],
  views: string,
  votes: string,
  answers : string,
  updationTime: Date ,
}