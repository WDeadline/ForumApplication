import { Component, OnInit } from '@angular/core';
import {Tag} from '../discussion/model/tag';
import {TagService} from './tag.service';

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.css']
})
export class TagComponent implements OnInit {

  tags : Tag[];
  tagView : Tag[];
  constructor(
    private tagService :TagService,
  ) { }

  ngOnInit() {
    this.getAllTag();
  }

  getAllTag(){
    this.tagService.getAllTag()
      .subscribe(data => {
        this.tags = data;
        this.tagView = data;
        console.log("get all tag successful");
      })
  }

  onSearchChange(searchValue : string){
    if(searchValue != ''){
      this.tagView = this.tags.filter(u => u.value.toLowerCase().indexOf(searchValue.toLowerCase())> -1);
    }else{
      this.tagView = this.tags;
    }
    console.log(searchValue);
  }


}
