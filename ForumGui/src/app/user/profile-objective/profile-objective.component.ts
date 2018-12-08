import { Component, OnInit } from '@angular/core';
import {ProfileObjectiveService} from './profile-objective.service';
import {Objective} from '../model1/objective';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';

@Component({
  selector: 'app-profile-objective',
  templateUrl: './profile-objective.component.html',
  styleUrls: ['./profile-objective.component.css']
})
export class ProfileObjectiveComponent implements OnInit {

  objectiveFormAdd: FormGroup;
  objectiveFormEdit: FormGroup;
  isEdit = false;
  isAdd = false;
  submittedAdd = false;
  submittesEdit = false;
  newObjective : Objective = new Objective();
  editObjective: Objective = new Objective();
  objectives : Objective[];
  constructor(
    private profileObjectiveService : ProfileObjectiveService,
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
  ) { }

  ngOnInit() {
    this.getObjectives();
    this.objectiveFormAdd = this.formBuilder.group({
      descriptionAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],      
    });



  }

  get f() { return this.objectiveFormAdd.controls; }

  get formEdit() { return this.objectiveFormEdit.controls; }

  addObjective(){

    this.submittedAdd = true;

    if (this.objectiveFormAdd.invalid) {
      return;
    }
    let des = this.f.descriptionAdd.value;
    this.newObjective.description = des;
    this.profileObjectiveService.addObjective(this.newObjective)
      .subscribe(data => {
        console.log("add Objective sussectfule");
        this.objectives.push(data);
        this.objectiveFormAdd = this.formBuilder.group({
          descriptionAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],      
        });
        this.isAdd = false;
        this.submittedAdd = false;
      });
  }

  //get tat ca Objective của các users
  getObjectives(){
    this.profileObjectiveService.getObjectives()
      .subscribe(data => this.objectives = data);
  }

  edit(objective){
    this.editObjective=objective;
    this.setValidation(this.editObjective.description);
    console.log("this.editObjective.dis: "+this.editObjective.description);
  }

  setValidation(description: string){
    this.objectiveFormEdit = this.formBuilder.group({
      descriptionEdit : [ description ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
    }); 
  }

  updateObjective(){
    this.submittesEdit = true;
    if (this.objectiveFormEdit.invalid) {
      return;
    }
    if(this.editObjective){
      let des = this.formEdit.descriptionEdit.value;
      this.editObjective.description = des;
      this.profileObjectiveService.updateObjective(this.editObjective)
      .subscribe(data => {
        console.log("edit objective succesfull");
        this.isEdit = false;
      });
    }

  }

  deleteObjective(objective: Objective){
    this.objectives = this.objectives.filter(h => h !== objective);
    this.profileObjectiveService.deleteObject(objective.id)
      .subscribe(data =>{
        console.log("delete objective succesfull");
      })
  }


  

}
