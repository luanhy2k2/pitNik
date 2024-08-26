import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from './User.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private userService:UserService) {}

  canActivate(): boolean {
    const token = this.userService.getUser().token
    
    if (!token) {
      this.router.navigate(['/login']); 
      return false; 
    }

    return true; 
  }
}
