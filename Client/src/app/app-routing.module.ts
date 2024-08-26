import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './Modules/index/index.component';
import { AccountSettingComponent } from './Modules/account-setting/account-setting.component';
import { AboutComponent } from './Modules/about/about.component';
import { FriendComponent } from './Modules/friend/friend.component';
import { LoginComponent } from './Modules/login/login.component';
import { SearchComponent } from './Modules/search/search.component';
import { TimelineGroupComponent } from './Modules/timeline-group/timeline-group.component';
import { GroupComponent } from './Modules/group/group.component';
import { MemberComponent } from './Modules/group/member/member.component';
import { InvitationComponent } from './Modules/group/invitation/invitation.component';
import { RegisterComponent } from './Modules/register/register.component';
import { ImagesComponent } from './Modules/about/images/images.component';
import { PostComponent } from './Modules/about/post/post.component';
import { VideoComponent } from './Modules/video/video.component';
import { AuthGuard } from './services/auth-guard.service';
import { LayoutComponent } from './Layout/layout/layout.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent, 
    canActivate:[AuthGuard],
    children: [
      {
        path: '',
        component: IndexComponent,
       
        title: 'Pinik'
      },
      { path: 'post/:id', component: IndexComponent,canActivate:[AuthGuard], },
      {
        path: 'setting',
        component: AccountSettingComponent,
       
      },
      {
        path: 'about',
        component: AboutComponent,
       
      },
      {
        path: 'friends',
        component: FriendComponent,
       
      },
      {
        path: 'search',
        component: SearchComponent,
       
      },
      {
        path: 'groups',
        component: TimelineGroupComponent,
       
      },
      {
        path: 'group',
        component: GroupComponent,
      },
      {
        path: 'images',
        component: ImagesComponent,
       
      },
      {
        path: 'Post',
        component: PostComponent,
       
      },
      {
        path: 'video',
        component: VideoComponent,
      },
      {
        path: 'groupMember',
        component: MemberComponent,
       
      },
      {
        path: 'groupInvitation',
        component: InvitationComponent,
       
      },
    ]
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
