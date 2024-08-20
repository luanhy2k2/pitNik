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
  constructor(private router: Router,
    private readonly presenceService:PresenceService,
    private activatedRoute: ActivatedRoute) {}
  ngOnInit() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      const currentRoute = this.activatedRoute.snapshot.firstChild;
      this.showHeader = currentRoute?.routeConfig?.path !== 'login' && currentRoute?.routeConfig?.path !== 'register';

    });
  }
  @HostListener('window:beforeunload', ['$event'])
  unloadHandler(event: Event): void {
    this.presenceService.stopConnection();
  }
  
}
