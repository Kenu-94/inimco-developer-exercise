import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonResponse } from '../../models/person.model';

@Component({
  selector: 'app-result-display',
  standalone: true,
  imports: [CommonModule], // nodig voor de json-pipe
  templateUrl: './result-display.html',
  styleUrl: './result-display.scss'
})
export class ResultDisplayComponent {
  @Input() result: PersonResponse | null = null;
}