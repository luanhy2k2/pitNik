import { Component } from '@angular/core';
import { PresenceService } from 'src/app/services/presence.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent {
  constructor(private readonly presenceService:PresenceService){}
  ngOnInit(){
    this.presenceService.startConnection();
  }
}
