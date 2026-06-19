import { Component, EventEmitter, Output } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PersonRequest } from '../../models/person.model';

@Component({
  selector: 'app-person-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './person-form.html',
  styleUrl: './person-form.scss'
})
export class PersonFormComponent {
  @Output() formSubmit = new EventEmitter<PersonRequest>();

  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      firstName: ['', [Validators.required, Validators.pattern('^[a-zA-Z]+$')]],
      lastName: ['', [Validators.required, Validators.pattern('^[a-zA-Z]+$')]],
      socialSkills: this.fb.array([this.fb.control('')]),
      socialAccounts: this.fb.array([this.createAccountGroup()])
    });
  }

  get socialSkills(): FormArray {
    return this.form.get('socialSkills') as FormArray;
  }

  get socialAccounts(): FormArray {
    return this.form.get('socialAccounts') as FormArray;
  }

  createAccountGroup(): FormGroup {
    return this.fb.group({
      type: ['', Validators.required],
      address: ['', Validators.required]
    });
  }

  addSkill(): void { this.socialSkills.push(this.fb.control('')); }
  removeSkill(i: number): void { this.socialSkills.removeAt(i); }
  addAccount(): void { this.socialAccounts.push(this.createAccountGroup()); }
  removeAccount(i: number): void { this.socialAccounts.removeAt(i); }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const raw = this.form.value;
    const request: PersonRequest = {
      firstName: raw.firstName,
      lastName: raw.lastName,
      socialSkills: raw.socialSkills.filter((s: string) => s.trim() !== ''),
      socialAccounts: raw.socialAccounts.filter((a: any) => a.type && a.address)
    };

    this.formSubmit.emit(request);
  }
}