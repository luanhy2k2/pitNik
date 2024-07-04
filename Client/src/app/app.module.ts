import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexComponent } from './Modules/index/index.component';
import { HeaderComponent } from './Partial/header/header.component';
import { FooterComponent } from './Partial/footer/footer.component';
import { AccountSettingComponent } from './Modules/account-setting/account-setting.component';
import { AboutComponent } from './Modules/about/about.component';
import { FriendComponent } from './Modules/friend/friend.component';
import { UserProfileComponent } from './Partial/user-profile/user-profile.component';
import { LoginComponent } from './Modules/login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SearchComponent } from './Modules/search/search.component';
import { SignalRService } from './services/signal-rservice.service';


export function initializeApp(signalrService: SignalRService) {
  return () => signalrService.startConnection();
}
@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    HeaderComponent,
    FooterComponent,
    AccountSettingComponent,
    AboutComponent,
    FriendComponent,
    UserProfileComponent,
    LoginComponent,
    SearchComponent,
   
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      multi: true,
      deps: [SignalRService]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
