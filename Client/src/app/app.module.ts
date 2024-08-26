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
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { SearchComponent } from './Modules/search/search.component';
import { PickerModule } from '@ctrl/ngx-emoji-mart';
import { TimelineGroupComponent } from './Modules/timeline-group/timeline-group.component';
import { GroupComponent } from './Modules/group/group.component';
import { MemberComponent } from './Modules/group/member/member.component';
import { GroupProfileComponent } from './Partial/group-profile/group-profile.component';
import { InvitationComponent } from './Modules/group/invitation/invitation.component';
import { SidebarLeftComponent } from './Partial/sidebar-left/sidebar-left.component';
import { SidebarRightComponent } from './Partial/sidebar-right/sidebar-right.component';
import { RegisterComponent } from './Modules/register/register.component';
import { ImagesComponent } from './Modules/about/images/images.component';
import { PostComponent } from './Modules/about/post/post.component';
import { VideoComponent } from './Modules/video/video.component';
import { AuthInterceptor } from './Interceptor/auth-interceptor.service';
import { LayoutComponent } from './Layout/layout/layout.component';

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
    TimelineGroupComponent,
    GroupComponent,
    MemberComponent,
    GroupProfileComponent,
    InvitationComponent,
    SidebarLeftComponent,
    SidebarRightComponent,
    RegisterComponent,
    ImagesComponent,
    PostComponent,
    VideoComponent,
    LayoutComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    PickerModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
