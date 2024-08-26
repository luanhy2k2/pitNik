import { Component, HostListener, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';
import { PresenceService } from './services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  showHeader = true;
  constructor( private readonly presenceService:PresenceService,) {}
 
  @HostListener('window:beforeunload', ['$event'])
  unloadHandler(event: Event): void {
    this.presenceService.stopConnection();
  }
  
}
