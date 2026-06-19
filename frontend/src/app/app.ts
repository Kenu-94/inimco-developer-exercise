import { Component } from '@angular/core';
import { PersonFormComponent } from './components/person-form/person-form';
import { ResultDisplayComponent } from './components/result-display/result-display';
import { PersonService } from './services/person.service';
import { PersonRequest, PersonResponse } from './models/person.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PersonFormComponent, ResultDisplayComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class AppComponent {
  result: PersonResponse | null = null;
  errorMessage: string | null = null;

  constructor(private personService: PersonService) {}

  onFormSubmit(request: PersonRequest): void {
    this.errorMessage = null;
    this.personService.analyze(request).subscribe({
      next: (response) => this.result = response,
      error: (err) => this.errorMessage = 'Er ging iets mis: ' + (err.error?.title ?? err.message)
    });
  }
}