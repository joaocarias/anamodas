import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DataService } from '../../../services/data/data.service';
import { CommonModule } from '@angular/common';
import { MaskDirective } from 'src/app/directives/mask.directive';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [ FormsModule, ReactiveFormsModule, CommonModule ],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent implements OnInit{
  public form: FormGroup;
  
  constructor(
    private dataService: DataService,
    private fb: FormBuilder 
      ) {

    this.form = this.fb.group({

      nome: ['', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(200),        
        Validators.required
      ])],
      
      email: ['', Validators.compose([
        Validators.required,
        Validators.email,
        Validators.min(3),
        Validators.max(255)
      ])],

      telefone: ['', Validators.compose([
        Validators.minLength(8),
        Validators.maxLength(20),        
        Validators.required
      ])],

      mensagem: ['', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(1000),        
        Validators.required
      ])]

    });
  }

  
  ngOnInit(): void {
    
  }

  submit(){

  }

  private enviarMensagem(){

  }

}
